import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { Profile, Profiles } from '../../interfaces/types';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { StepsModule } from 'primeng/steps';
import { PersonalInfoFormComponent } from './personal-info-form/personal-info-form.component';
import { TagsFormComponent } from './tags-form/tags-form.component';
import { Constants } from '../../../constants/constants';
import { FileService } from '../../services/files.service';
import { ExperienceComponent } from './experience/experience.component';
import { ProjectComponent } from './project/project.component';
import { CardModule } from 'primeng/card';


@Component({
  selector: 'app-profile-create-update',
  standalone: true,
  imports: [CommonModule, DialogModule, ButtonModule, StepsModule, PersonalInfoFormComponent, CardModule, ProjectComponent, TagsFormComponent, ExperienceComponent],
  templateUrl: './profile-create-update.component.html',
  styleUrls: ['./profile-create-update.component.css'],
})

export class ProfileCreateUpdateComponent {
  @Input() profileId = 0;
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() profileChangeCallBack = new EventEmitter<Profile>();
  experienceType: string = '';
  type: string = 'profile';

  header: String = Constants.createProfile;

  steps = [
    { label: Constants.stepPersonalInfo },
    { label: Constants.stepTags },
    { label: Constants.stepEducation },
    { label: Constants.stepEmployment },
    { label: Constants.stepProjects },
  ];

  activeStepIndex = 0;
  profile = {} as Profile;

  constructor(private profileService: ProfileService, private fileService: FileService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['visible'] && this.visible) {
      this.openModal();
    }
  }

  openModal(): void {
    if (this.profileId === 0) {
      this.resetProfile();
      this.header = Constants.createProfile;
    } else {
      this.fetchData();
      this.header = Constants.updateProfile;
    }
  }

  fetchData(): void {
    this.profileService.getProfile(this.profileId).subscribe((item: Profiles) => {
      this.profile = item.profiles[0];

      this.profile.education = [];
      this.profile.employment = [];

      this.profile.experiences.forEach((experience) => {
        if (experience.type === 'Education') {
          this.profile.education.push(experience);
        } else {
          this.profile.employment.push(experience);
        }
      });
    });
  }

  resetProfile() {
    this.profile = {} as Profile;
    this.profile.education = [];
    this.profile.employment = [];
    this.profile.projects = [];
    this.profile.tags = [];
    this.profile.experiences = [];
  }

  nextStep() {
    if (this.activeStepIndex < this.steps.length - 1) {
      this.activeStepIndex++;
      this.checkStep();
    }
  }

  deleteExperience(id: number): void {
    this.profile.education = this.profile.education.filter((exp) => exp.id !== id);
    this.profile.employment = this.profile.employment.filter((exp) => exp.id !== id);
  }

  deleteProject(id: number): void {
    this.profile.projects = this.profile.projects.filter((p) => p.id !== id);
  }

  prevStep() {
    if (this.activeStepIndex > 0) {
      this.activeStepIndex--;
      this.checkStep();
    }
  }

  updateProfile(profile: Profile) {
    this.profile = profile;
  }

  checkStep() {
    switch (this.activeStepIndex) {
      case 2:
        this.experienceType = 'Education';
        break;
      case 3:
        this.experienceType = 'Job';
        break;
      default:
        break;
    }
  }

  saveProfile() {
    this.close();
  }

  close(): void {
    this.visible = false;
    this.resetProfile();
    this.activeStepIndex = 0;
    this.profileChangeCallBack.emit(this.profile);
    this.visibleChange.emit(false);
  }
}
