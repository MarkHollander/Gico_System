import { Component, OnInit, ViewChild } from '@angular/core';
import { FileUploadComponent } from '../../components/file-upload/file-upload.component';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild(FileUploadComponent) fileUpload: FileUploadComponent;
  constructor() { }

  ngOnInit() {
  }

  onSave() {
    let img = this.fileUpload.imagePath;
    console.log(img);
  }
}
