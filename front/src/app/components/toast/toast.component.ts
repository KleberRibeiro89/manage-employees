import { Component, input, Input } from '@angular/core';

@Component({
  selector: 'app-toast',
  imports: [],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss'
})
export class ToastComponent {
  @Input() title = '';
  @Input() message = '';


  ngOnInit(): void {

  }

}
