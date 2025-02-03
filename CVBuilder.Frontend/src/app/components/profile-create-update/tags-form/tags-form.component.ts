import { Component, Input, SimpleChanges } from '@angular/core';
import { Tag } from '../../../interfaces/types';
import { TagService } from '../../../services/tag.service';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { ProfileService } from '../../../services/profile.service';
import { Messages } from '../../../../constants/constants';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { FloatLabel } from 'primeng/floatlabel';
import { Message } from 'primeng/message';
import { FeedbackMessagesComponent } from '../../feedback-messages/feedback-messages.component';
import { ProjectService } from '../../../services/project.service';
import { MessageService } from 'primeng/api';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { TagModule } from 'primeng/tag';


@Component({
  selector: 'app-tags-form',
  templateUrl: './tags-form.component.html',
  standalone: true,
  imports: [TagModule, FloatLabel, AutoCompleteModule, CommonModule, DialogModule, ButtonModule, InputTextModule, FormsModule, FloatLabel, Message, FeedbackMessagesComponent],
  styleUrl: './tags-form.component.css'
})

export class TagsFormComponent {
  @Input() tags: Tag[] = [];
  @Input() id: number = 0;
  @Input() type: string = '';
  filteredTags: Tag[] = [];
  allTags: Tag[] = [];
  selectedTag = {} as Tag;
  error: any = null;
  loading: boolean = false;

  constructor(private tagService: TagService, private profileService: ProfileService, private projectService: ProjectService, private messageService: MessageService) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.fetchAllTags();
    if (!this.tags) {
      this.tags = [];
    }
  }

  private fetchAllTags(): void {
    this.tagService.getTags().subscribe({
      next: (response: { tags: Tag[] }) => {
        this.allTags = response.tags;
        this.filteredTags = this.allTags;
      },
      error: (error) => this.error = error
    });
  }

  onTagsChange(): void {
    const tagIds = this.tags.map(tag => tag.id);

    if (this.type === 'profile') {
      this.updateProfileTags(tagIds);
    }
    if (this.type === 'project') {
      this.updateProjectTags(tagIds);
    }
  }

  updateProfileTags(tagIds: number[]): void {
    this.profileService.updateProfileTags(tagIds, this.id).subscribe({
      next: () => this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.tagUpdated}` }),
      error: (error) => this.error = error
    });
  }

  updateProjectTags(tagIds: number[]): void {
    this.projectService.updateProjectTags(tagIds, this.id).subscribe({
      next: () => this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.tagUpdated}` }),
      error: (error) => this.error = error
    });
  }

  filterTags(event: any) {
    this.error = null;
    const query = event.query;
    this.filteredTags = this.allTags.filter(tag =>
      tag.title.toLowerCase().includes(query.toLowerCase())
    );
  }

  addTag() {
    if (typeof this.selectedTag === 'string') {
      this.selectedTag = { title: this.selectedTag, id: 0 };
    }

    const trimmedTagName = this.selectedTag.title.trim().toLowerCase();

    if (!trimmedTagName) {
      this.error = Messages.tagCannotBeEmpty;
      return;
    }

    this.loading = true;

    const existingTag = this.allTags.find(tag => tag.title.toLowerCase() === trimmedTagName);
    if (existingTag) {
      this.handleExistingTag(existingTag);
      return;
    }

    this.createNewTag(this.selectedTag.title.trim());
  }

  private handleExistingTag(tag: Tag): void {
    if (this.tags.some(selected => selected.id === tag.id)) {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.tagAlreadySelected}` });
      this.loading = false;
      return;
    }
    this.tags.push(tag);
    this.loading = false;
    this.onTagsChange();
  }

  private createNewTag(tagName: string): void {
    this.tagService.postTag(tagName).subscribe({
      next: (response) => {
        const newTag = { id: response, title: tagName };
        this.allTags.push(newTag);
        this.tags.push(newTag);
        this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.tag} ${Messages.createComplete}` });
      },
      error: (error) => this.error = error,
      complete: () => {
        this.onTagsChange();
        this.loading = false
      }
    });
  }

  removeTag(tag: Tag) {
    this.tags = this.tags.filter(selected => selected.id !== tag.id);
    this.onTagsChange();
  }
}
