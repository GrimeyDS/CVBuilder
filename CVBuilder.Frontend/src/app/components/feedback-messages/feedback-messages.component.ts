import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { Message } from 'primeng/message';
import { Messages } from '../../../constants/constants';

@Component({
  selector: 'app-feedback-messages',
  templateUrl: './feedback-messages.component.html',
  standalone: true,
  imports: [CommonModule, DialogModule, Message],
  styleUrl: './feedback-messages.component.css'
})
export class FeedbackMessagesComponent {
  rawError: any = null;

  errorMessages: string[] = [];

  resetMessages() {
    this.errorMessages = [];
  }

  @Input() set error(error: any) {
    this.resetMessages();
    this.rawError = error;
    if (error) {
      this.processErrors(error);
    }
  }

  processErrors(error: any) {
    let errorMessages: string[] = [];
    let errorDetail = Messages.error;

    if (typeof error === 'string') {
      errorMessages = [error];
      this.errorMessages = errorMessages;
      return;
    }

    try {
      const parsedErrors = error.error?.errors;
      if (parsedErrors) {
        for (const [field, messages] of Object.entries(parsedErrors)) {
          errorMessages.push(...(messages as string[]));
        }
      } else {
        const parsedErrors = JSON.parse(error.error);
        errorDetail = parsedErrors.detail || errorDetail;
        errorMessages = [errorDetail];
      }
    }
    catch (e) {
      console.error(Messages.apiError, e);
      errorMessages = [errorDetail];
    }

    this.errorMessages = errorMessages;
  }
}
