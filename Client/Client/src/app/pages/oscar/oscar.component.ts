import { Component, OnInit } from '@angular/core';
import { ResumeInfo } from '../../models/resume/resume_info';
import { ResumeService } from '../../services/resume/resume.service';

@Component({
  selector: 'app-oscar',
  templateUrl: './oscar.component.html',
  styleUrls: ['./oscar.component.scss']
})
export class OscarComponent implements OnInit {
  basicInfo: ResumeInfo = new ResumeInfo();

  constructor(private resumeService: ResumeService) {}

  ngOnInit(): void {
    this.getResumeInfo();
  }

  getResumeInfo() : void {
    this.resumeService.getBasicInfo()
        .subscribe(resumeInfo => this.basicInfo = resumeInfo);
  }

}
