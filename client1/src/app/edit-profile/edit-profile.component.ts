import { Component } from '@angular/core';
import { Input } from '@angular/core';
@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent {
  @Input() isOpen: boolean = false;

  closePopup() {
    this.isOpen = false;
  }

}
