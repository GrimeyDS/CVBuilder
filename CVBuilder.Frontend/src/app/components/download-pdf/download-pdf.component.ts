import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CommonModule } from '@angular/common';
import { DropdownModule } from 'primeng/dropdown';
import { PDFService } from '../../services/pdf.service';
import { Template, Templates } from '../../interfaces/types';
import { Select } from 'primeng/select';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-download-pdf',
  standalone: true,
  imports: [DialogModule, CommonModule, ButtonModule, DropdownModule, Select, FormsModule],
  templateUrl: './download-pdf.component.html',
  styleUrl: './download-pdf.component.css'
})
export class DownloadPdfComponent {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();

  @Input() profileId = 0;

  templates: Template[] = [];
  selectedTemplate: Template | null = null;
  loading = false;

  constructor(private pdfService: PDFService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['visible'] && this.visible) {
      this.fetchData();
    }
  }

  fetchData(): void {
    this.pdfService.getTemplates().subscribe((items: Templates) => {
      this.templates = items.templates;
    });
  }

  downloadPDF(): void {
    if (this.selectedTemplate && this.profileId) {
      this.pdfService.getPDF(this.profileId, this.selectedTemplate.id).subscribe((data: Blob) => {
        const url = window.URL.createObjectURL(data);
        const link = document.createElement('a');
        link.href = url;
        link.download = 'CV.pdf';
        link.click();
        window.URL.revokeObjectURL(url);
        this.close();
      });
    }
  }


  close(): void {
    this.visible = false;
    this.visibleChange.emit(this.visible);
  }
}
