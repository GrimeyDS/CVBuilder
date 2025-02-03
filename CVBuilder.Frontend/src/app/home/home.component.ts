import { Component } from '@angular/core';
import { ProfileService } from '../services/profile.service';
import { TagService } from '../services/tag.service';
import { Profile, Profiles, Tag, Tags } from '../interfaces/types';
import { FileService } from '../services/files.service';
import { CommonModule } from '@angular/common';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ProfileDetailsComponent } from '../components/profile-details/profile-details.component';
import { DownloadPdfComponent } from '../components/download-pdf/download-pdf.component';
import { ProgressSpinner } from 'primeng/progressspinner';
import { forkJoin, tap } from 'rxjs';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { Constants, Messages } from '../../constants/constants';
import { ProfileCreateUpdateComponent } from '../components/profile-create-update/profile-create-update.component';
import { Toast } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { FloatLabel } from 'primeng/floatlabel';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FloatLabel, Toast, IconFieldModule, ConfirmDialog, TagModule, MultiSelectModule, InputIconModule, InputTextModule, FormsModule, TableModule, ProfileDetailsComponent, DownloadPdfComponent, ButtonModule, ProgressSpinner, ProfileCreateUpdateComponent],
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  profiles: Profile[] = [];
  tags: Tag[] = [];

  filteredProfiles: Profile[] = [];
  filterName: string = '';
  filterYears: number = 0;
  selectedTags: Tag[] = [];

  selectedProfileId: number = 0;
  isProfileDetailsVisible: boolean = false;
  isPDFDownloadVisible: boolean = false;
  isCreateEditProfileVisible: boolean = false;
  loading: boolean = false;

  constructor(private profileService: ProfileService, private tagService: TagService, private fileService: FileService, private confirmationService: ConfirmationService, private messageService: MessageService) { }
  ngOnInit() {
    this.fetchData();
  }

  ngOnChanges() {
    this.fetchData();
  }

  fetchData(): void {
    this.loading = true;

    const tags = this.tagService.getTags().pipe(
      tap((items: Tags) => {
        this.tags = items.tags;
      })
    );

    const profiles = this.profileService.getProfiles().pipe(
      tap((items: Profiles) => {
        const profileObservables = items.profiles.map((profile) =>
          this.fileService.getFile(Constants.profilePictureContainer, profile.pictureUrl).pipe(
            tap({
              next: (blob) => {
                profile.pictureUrl = URL.createObjectURL(blob);
              },
              error: (err) => {
                console.error(Messages.failedToFetchPicture, err);
              },
            })
          )
        );

        forkJoin(profileObservables).subscribe({
          complete: () => {
            this.profiles = items.profiles;
            this.filteredProfiles = items.profiles;
          },
        });
      })
    );

    forkJoin([tags, profiles]).subscribe({
      complete: () => {
        this.loading = false;
      },
      error: (err) => {
        console.error(Messages.apiError, err);
        this.loading = false;
      },
    });
  }

  showProfile(profileId: number): void {
    this.selectedProfileId = profileId;
    this.isProfileDetailsVisible = true;
  }

  showPDFDownload(profileId: number): void {
    this.selectedProfileId = profileId;
    this.isPDFDownloadVisible = true;
  }

  editProfile(profileId: number): void {
    this.selectedProfileId = profileId;
    this.isCreateEditProfileVisible = true;
  }

  createNewProfile(): void {
    this.selectedProfileId = 0;
    this.isCreateEditProfileVisible = true;
  }

  updateList(profile: Profile): void {
    this.selectedProfileId = profile.id;
    this.fetchData();
  }

  applyFilter(): void {
    this.loading = true;
    const lowerCaseName = this.filterName.toLowerCase();
    this.filteredProfiles = this.profiles.filter((item) =>
      `${item.firstName} ${item.lastName}`.toLowerCase().includes(lowerCaseName)
    );

    if (this.filterYears > 0) {
      this.filteredProfiles = this.filteredProfiles.filter((item) => item.yearsOfExperience >= this.filterYears);
    }

    if (this.selectedTags?.length > 0) {
      this.filteredProfiles = this.filteredProfiles.filter((item) => {
        const tags = item.tags?.map((tag) => tag.id || []);
        return this.selectedTags.every((selectedTag) =>
          tags.includes(selectedTag.id)
        );
      });
    }
    this.loading = false;
  }

  confirmDelete(profile: Profile) {
    this.confirmationService.confirm({
      message: `${Messages.deleteDialog} "${profile.firstName} ${profile.lastName}"?`,
      acceptLabel: 'Delete',
      rejectLabel: 'Cancel',
      acceptIcon: 'pi pi-trash',
      rejectIcon: 'pi pi-times',
      acceptButtonStyleClass: 'p-button-danger',
      rejectButtonStyleClass: 'p-button-secondary',
      header: Messages.confirmDelete,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteProfile(profile.id);
      }
    });
  }

  deleteProfile(id: number) {
    this.profileService.deleteProfile(id).subscribe({
      next: () => {
        this.profiles = this.profiles.filter((profile) => profile.id !== id);
        this.filteredProfiles = this.filteredProfiles.filter((profile) => profile.id !== id);
      },
    });
    this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.profile} ${Messages.deleteComplete}` });
  }
}
