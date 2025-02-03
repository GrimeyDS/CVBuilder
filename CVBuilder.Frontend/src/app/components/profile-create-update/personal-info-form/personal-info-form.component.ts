import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { FloatLabel } from 'primeng/floatlabel';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Message } from 'primeng/message';
import { InputTextModule } from 'primeng/inputtext';
import { DatePicker } from 'primeng/datepicker';
import { TextareaModule } from 'primeng/textarea';
import { FileUpload } from 'primeng/fileupload';
import { ButtonModule } from 'primeng/button';
import { FileService } from '../../../services/files.service';
import { ProfileService } from '../../../services/profile.service';
import { ProgressSpinner } from 'primeng/progressspinner';
import { Messages, Constants } from '../../../../constants/constants';
import { FeedbackMessagesComponent } from '../../feedback-messages/feedback-messages.component';
import { Profile } from '../../../interfaces/types';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-personal-info-form',
  imports: [FloatLabel, CommonModule, FeedbackMessagesComponent, FormsModule, Message, ReactiveFormsModule, InputTextModule, DatePicker, TextareaModule, FileUpload, ButtonModule, ProgressSpinner],
  templateUrl: './personal-info-form.component.html',
  standalone: true,
  styleUrl: './personal-info-form.component.css'
})
export class PersonalInfoFormComponent {
  @Input() profile = {} as Profile;
  form: FormGroup;
  error: any = null;
  @Output() profileChangeCallBack = new EventEmitter<Profile>();

  file: any;
  loading: boolean = false;
  profileImageUrl: string = '';

  constructor(private fb: FormBuilder, private fileService: FileService, private profileService: ProfileService, private messageService: MessageService) {
    this.form = this.fb.group({
      id: [null],
      firstName: [''],
      lastName: [''],
      birthDate: [null],
      description: [''],
      picture: [null],
      pictureName: [''],
      currentRole: [''],
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['profile'] && this.profile) {

      const birthDate = this.profile.birthDate;
      this.file = null;

      this.form.patchValue({
        id: this.profile.id,
        firstName: this.profile.firstName,
        lastName: this.profile.lastName,
        birthDate: birthDate ? new Date(birthDate) : null,
        description: this.profile.description,
        pictureName: this.profile.pictureUrl,
        currentRole: this.profile.currentRole
      });

      if (!this.profile.pictureUrl) {
        this.profile.pictureUrl = Constants.profilePlaceholderImage;
      }

      this.fileService.getFile(Constants.profilePictureContainer, this.profile.pictureUrl)
        .subscribe({
          next: (response) => {
            this.profileImageUrl = URL.createObjectURL(response);
          },
          error: (error) => {
            this.error = error;
          }
        });
    }
  }

  async onSave() {
    this.loading = true;
    if (this.file) {
      const isUploaded = await this.handleFileUpload();
      if (isUploaded)
        this.handleAPIRequest();
    }
    else
      this.handleAPIRequest();
    this.loading = false;
  }

  handleFileUpload(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.fileService
        .postFile(Constants.profilePictureContainer, this.file, this.form.get('pictureName')?.value)
        .subscribe({
          next: (response) => {
            this.form.patchValue({ pictureName: response });
            this.profileImageUrl = URL.createObjectURL(this.file);
            this.profile.pictureUrl = response;
            resolve(true);
          },
          error: (error) => {
            this.error = error;
            reject(error);
          },
        });
    });
  }

  handleAPIRequest() {
    this.error = null;

    this.updateProfileModel();

    if (this.profile.id) {
      this.profileService.putProfile(this.profile, this.profile.id).subscribe({
        next: (success) => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.profile} ${Messages.updateComplete}` });
          this.profileChangeCallBack.emit(this.profile);
          },
        error: (error) => this.error = error
      });
    }
    else {
      // change behaviour when authentication is implemented
      this.profile.userId = 1;
      this.profileService.postProfile(this.profile).subscribe({
        next: (success) => {
          this.profile.id = success;
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.profile} ${Messages.createComplete}` });
          this.profileChangeCallBack.emit(this.profile);
        },
        error: (error) => this.error = error
      });
    }
  }

  updateProfileModel() {
    let formDate = this.form.get('birthDate')?.value;
    const birthDate = new Date(formDate as string);
    birthDate.setHours(birthDate.getHours() + 2);

    this.profile.firstName = this.form.get('firstName')?.value;
    this.profile.lastName = this.form.get('lastName')?.value;
    this.profile.birthDate = birthDate;
    this.profile.description = this.form.get('description')?.value;
    this.profile.pictureName = this.form.get('pictureName')?.value;
    this.profile.currentRole = this.form.get('currentRole')?.value;
  }

  onFileSelect(event: any) {
    this.file = event.files[0];
    this.form.patchValue({ picture: this.file });
    this.profileImageUrl = URL.createObjectURL(this.file);
  }
}
