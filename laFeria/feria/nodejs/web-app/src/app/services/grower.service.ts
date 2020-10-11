import { Injectable } from '@angular/core';
import { Grower } from '../models/grower.model';
import { HttpClient, HttpParams } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class GrowerService {

  constructor(private http: HttpClient) { }


  addGrower(id: string, info: string) {
    console.log(id);
    console.log(info);


    const params = new HttpParams()
    .set('info', info);


    console.log();


    this.http.post('https://localhost:44303/api/Productor/' + id+'?'+params.toString(), null).subscribe(data => {
      console.log(data);
    });

  }

}
