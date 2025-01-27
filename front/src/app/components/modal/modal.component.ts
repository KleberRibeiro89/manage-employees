import { CommonModule } from '@angular/common';
import { Component, ElementRef, Input, input, Output, output, ViewChild } from '@angular/core';

@Component({
  selector: 'app-modal',
  imports: [CommonModule],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss'
})
export class ModalComponent {

  @ViewChild('dialog') dialog!: ElementRef<HTMLDialogElement>;
  @Input() visible = false;

  ngOnChanges(): void {

    if (this.visible && this.dialog) {
      this.dialog.nativeElement.showModal();
    } else if (this.dialog) {
      this.dialog.nativeElement.close();
    }
  }


  ngAfterViewInit() {
    this.dialog.nativeElement.showModal();
  }

  closeModal() {
    this.visible = false;
  }

}
