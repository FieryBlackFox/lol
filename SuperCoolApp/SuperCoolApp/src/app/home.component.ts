﻿import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Http } from '@angular/http';
import { ImgService } from './img.service';

@Component({
    selector: 'home-app',
    templateUrl: './home.component.html',
    providers: [ImgService]
})


export class HomeComponent {

    @ViewChild("fileInput") fileInput;
    private imgService: ImgService;
    constructor(private router: Router, private http: Http) {
        this.imgService = new ImgService(this.http);
    }
    addFile() {
        let fi = this.fileInput.nativeElement;
        if (fi.files) {
            let fileToUpload = fi.files;
            this.imgService
                .upload(fileToUpload)
                .subscribe(res => {
                    if (res.status == 201)
                    {
                        console.log(res.url + " " + res.statusText);
                    }
                    else
                    {
                        console.log("G");
                    }
                    console.log(res.status);
                });
        }
    }
}