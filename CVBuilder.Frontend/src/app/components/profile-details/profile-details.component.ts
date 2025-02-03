import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { CommonModule } from '@angular/common';
import { Profile, Profiles } from '../../interfaces/types';
import { ProfileService } from '../../services/profile.service';
import { ButtonModule } from 'primeng/button';
import { Chip } from 'primeng/chip';
import { TagModule } from 'primeng/tag';
import { ProfileCreateUpdateComponent } from '../profile-create-update/profile-create-update.component';
import { CardModule } from 'primeng/card';
import { DividerModule } from 'primeng/divider';
import { FileService } from '../../services/files.service';
import { DownloadPdfComponent } from '../../components/download-pdf/download-pdf.component';
import { ProgressSpinner } from 'primeng/progressspinner';
import { catchError, forkJoin, of, tap } from 'rxjs';
import { Constants, Messages } from '../../../constants/constants';

@Component({
  selector: 'app-profile-details',
  standalone: true,
  imports: [DialogModule, CommonModule, ButtonModule, Chip, TagModule, CardModule, DividerModule, DownloadPdfComponent, ProfileCreateUpdateComponent, ProgressSpinner],
  templateUrl: './profile-details.component.html',
  styleUrl: './profile-details.component.css'
})
export class ProfileDetailsComponent {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();

  isUpdateModalVisible: boolean = false;
  selectedMode: String = 'edit';

  @Input() profileId = 0;
  profile = {} as Profile;

  isPDFDownloadVisible: boolean = false;
  loading: boolean = false;

  constructor(private profileService: ProfileService, private fileService: FileService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['visible'] && this.visible) {
      this.fetchData();
    }
  }

  fetchData(): void {
    this.loading = true;

    this.profileService.getProfile(this.profileId).subscribe({
      next: (item: Profiles) => {
        const profile = item.profiles[0];

        const profilePictureObservable = this.fileService.getFile(Constants.profilePictureContainer, profile.pictureUrl).pipe(
          tap((blob) => {
            profile.pictureUrl = URL.createObjectURL(blob);
          }),
          catchError((err) => {
            console.error(Messages.failedToFetchPicture, err);
            return of(null);
          })
        );

        const projectPictureObservables = profile.projects.map((project) =>
          this.fileService.getFile(Constants.projectPictureContainer, project.pictureUrl).pipe(
            tap((blob) => {
              project.pictureUrl = URL.createObjectURL(blob);
            }),
            catchError((err) => {
              console.error(Messages.failedToFetchPicture, err);
              return of(null); 
            })
          )
        );

        forkJoin([profilePictureObservable, ...projectPictureObservables]).subscribe({
          complete: () => {
            this.profile = profile;
            this.loading = false;
            this.profile.education = [];
            this.profile.employment = [];

            this.profile.experiences.forEach((experience) => {
              if (experience.type === 'Education') {
                this.profile.education.push(experience);
              } else {
                this.profile.employment.push(experience);
              }
            });
            
          },
        });
      },
      error: (err) => {
        console.error(Messages.failedToFetchProfile, err);
        this.loading = false;
      },
    });
  }

  openUpdateProfileModal() {
    this.isUpdateModalVisible = true;
    this.close();
  }

  showPDFDownload(): void {
    this.isPDFDownloadVisible = true;
  }

  close(): void {
    this.visibleChange.emit(false);
    this.visible = false;
  }
}
