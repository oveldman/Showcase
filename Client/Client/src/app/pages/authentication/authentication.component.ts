import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Bearer } from 'src/app/models/authentication/bearer';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss']
})
export class AuthenticationComponent implements OnInit {
  public showError : boolean = false;
  public IsLoggedIn : boolean = false;
  public IsNotLoggedIn : boolean = true;
  form:FormGroup;

  constructor(private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private router: Router) { 
      this.form = this.formBuilder.group({
        username: ['',Validators.required],
        password: ['',Validators.required]
    });
   }

  ngOnInit(): void {
    const idToken = localStorage.getItem("id_token");
    this.IsLoggedIn = idToken !== null;
    this.IsNotLoggedIn = !this.IsLoggedIn;
  }

  login(customerData) {

    if (customerData.username && customerData.password) {
        this.authenticationService.login(customerData.username, customerData.password).subscribe(bearer => {
          this.showError = !bearer.Succes;

          if (bearer.Succes) {
            this.router.navigateByUrl('/oscar');
          } 
        });
    }
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigateByUrl('/oscar');
  }

}
