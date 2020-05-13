import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'registration';
  
  //registrationForm: formGroup;  
  
  public firstName: string = "";
  public lastName: string = "";
  public npiNumber: string = "";
  public businessStreet: string = "";
  public businessCity: string = "";
  public businessState: string = "";
  public businessZip: string = "";
  public telephoneNumber: string = "";
  public emailAddress: string = "";

  onReset() {
    this.firstName = "";
    this.lastName = "";
    this.npiNumber = "";
    this.businessStreet = "";
    this.businessCity = "";
    this.businessState = "";
    this.businessZip = "";
    this.telephoneNumber = "";
    this.emailAddress = "";
  }
  onSubmit() {
    console.warn("click");
  }
}
