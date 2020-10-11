import { Injectable } from '@angular/core';
import { Grower } from '../models/grower.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GrowerService {

  constructor(private http: HttpClient) { }


  getGrower(id: string) {

    const Base_url = 'https://localhost:44303/api/Productor';

    const params = new HttpParams()
      .set('id', id);

    //return this.http.get(Base_url + '?id=' + id).map((response: any) => response.json());

    return this.http.get(Base_url + '?id=' + id).
      pipe(
        map((data: any) => {
          return data;
        }), catchError(error => {
          return throwError('Something went wrong!');
        })
      )

    /*this.http.get(Base_url + '?id=' + id).subscribe(data => {
      console.log(data);
      
      return data;
    });*/

  }

  addGrower(id: string, info: string) {
    console.log(id);
    console.log(info);

    const Base_url = 'https://localhost:44303/api/Productor/';

    const params = new HttpParams()
    .set('info', info);


    this.http.post(Base_url + id+'?'+params.toString(), null).subscribe(data => {
      console.log(data);
    });

  }

}
