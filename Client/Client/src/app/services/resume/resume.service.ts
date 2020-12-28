import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ResumeInfo} from '../../models/resume/resume_info'

@Injectable({
  providedIn: 'root'
})

export class ResumeService {

  constructor(private http: HttpClient) { }

  getBasicInfo() : Observable<ResumeInfo> {
    var url: string = "api/resume";

    return this.http.get<ResumeInfo>(url);
  }
}
