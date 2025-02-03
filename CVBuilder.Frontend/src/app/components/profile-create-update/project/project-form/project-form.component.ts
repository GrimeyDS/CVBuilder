import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Message } from 'primeng/message';
import { FeedbackMessagesComponent } from '../../../feedback-messages/feedback-messages.component';
import { Constants, Messages } from '../../../../../constants/constants';
import { Project } from '../../../../interfaces/types';
import { TextareaModule } from 'primeng/textarea';
import { InputTextModule } from 'primeng/inputtext';
import { DatePicker } from 'primeng/datepicker';
import { ProjectService } from '../../../../services/project.service';
import { FileService } from '../../../../services/files.service';
import { FileUpload } from 'primeng/fileupload';
import { TagsFormComponent } from '../../tags-form/tags-form.component';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-project-form',
  standalone: true,
  imports: [ButtonModule, FormsModule, ReactiveFormsModule, InputTextModule, DatePicker, CommonModule, TagsFormComponent, DialogModule, Message, FeedbackMessagesComponent, TextareaModule, FileUpload],
  templateUrl: './project-form.component.html',
  styleUrl: './project-form.component.css'
})
export class ProjectFormComponent {
  @Input() project = {} as Project;
  @Input() profileId: number = 0;
  @Output() closeFormCallBack = new EventEmitter<any>();

  form: FormGroup;
  error: any = null;
  file: any;
  projectImageUrl: string = '';
  type: string = 'project';

  constructor(private fb: FormBuilder, private projectService: ProjectService, private fileService: FileService, private messageService: MessageService) {
    this.form = this.fb.group({
      title: [''],
      customer: [''],
      description: [''],
      startDate: [null],
      endDate: [null],
      picture: [null],
      pictureName: [''],
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['project']) {

      const endDate = this.project.endDate;
      const startDate = this.project.startDate;
      this.file = null;

      this.form.patchValue({
        title: this.project.title,
        customer: this.project.customer,
        description: this.project.description,
        startDate: startDate ? new Date(startDate) : null,
        endDate: endDate ? new Date(endDate) : null,
        pictureName: this.project.pictureUrl
      });

      if (!this.project.pictureUrl) {
        this.project.pictureUrl = Constants.projectPlaceholderImage;
      }

      this.fileService.getFile(Constants.projectPictureContainer, this.project.pictureUrl)
        .subscribe({
          next: (response) => {
            this.projectImageUrl = URL.createObjectURL(response);
          },
          error: (error) => {
            this.error = error;
          }
        });
      }
   }

  async onSave() {
    if (this.file) {
      const isUploaded = await this.handleFileUpload();
      if (isUploaded)
        this.handleAPIRequest();
    }
    else
      this.handleAPIRequest();
  }

  handleFileUpload(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.fileService
        .postFile(Constants.projectPictureContainer, this.file, this.form.get('pictureName')?.value)
        .subscribe({
          next: (response) => {
            this.form.patchValue({ pictureName: response });
            this.projectImageUrl = URL.createObjectURL(this.file);
            this.project.pictureUrl = response;
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

    this.updateProjectModel();

    if (this.project.id) {
      this.projectService.putProject(this.project, this.project.id).subscribe({
        next: (success) => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.project} ${Messages.updateComplete}` });
          this.project.id = success.id;
        },
        error: (error) => this.error = error
      });
    }
    else {
      this.project.profileId = this.profileId
      this.projectService.postProject(this.project).subscribe({
        next: (success) => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.project} ${Messages.createComplete}` });
          this.project.id = success;
          this.project.tags = [];
        },
        error: (error) => this.error = error
      });
    }
  }

  updateProjectModel(): void {
    let formStartDate = this.form.get('startDate')?.value;
    let formEndDate = this.form.get('endDate')?.value;
    const startDate = formStartDate ? new Date(formStartDate) : undefined;
    const endDate = formEndDate ? new Date(formEndDate) : undefined;

    this.project.title = this.form.get('title')?.value;
    this.project.customer = this.form.get('customer')?.value;
    this.project.description = this.form.get('description')?.value;
    this.project.startDate = startDate as Date;
    this.project.endDate = endDate;
    this.project.pictureName = this.form.get('pictureName')?.value;
  }

  onFileSelect(event: any) {
    this.file = event.files[0];
    this.form.patchValue({ picture: this.file });
    this.projectImageUrl = URL.createObjectURL(this.file);
  }

  onCancel() {
    this.closeFormCallBack.emit(this.project);
  }
}
