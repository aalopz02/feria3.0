import { Component, OnInit } from '@angular/core';
import { Grower } from '../../../models/grower.model';
import { GrowerService } from '../../../services/grower.service'
import { FormBuilder, FormControl, FormGroup, FormArray } from '@angular/forms'
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  id: string;
  private sub: any;
  growerData: any;
  growerform: FormGroup;
  updateGrower: boolean = false;
  
  constructor(private formB: FormBuilder, private growerService: GrowerService, private activatedRoute:ActivatedRoute) {
    
  }

  ngOnInit(): void {

    this.growerform = this.formB.group({
      name: [''],
      lastname1: [''],
      lastname2: [''],
      id: [''],
      phoneNumber: [''],
      simpenumber: [''],
      birthday: [''],
      deliveradress: [''],
      provincia: [''],
      canton: [''],
      distrito: ['']
    })

    this.activatedRoute.params.subscribe(params => {
      if (params['id'] == undefined) {
        return;
      }
      this.updateGrower = true;
    })

    if (this.updateGrower) {
      this.sub = this.activatedRoute.params.subscribe(params => {
        this.id = params['id'];
      });

      this.growerService.getGrower(this.id).subscribe(
        (data: any) => {
          //console.log(data.cedula);
          this.dataToUpdate(data);
          //this.users = data.data;
        })


       /* data => {
          this.growerData = data,
            console.log(this.growerData);
            console.log(this.growerData.nombre);
          
        }
      );*/
      //this.growerData = this.growerService.getGrower(this.id);
      //console.log(this.growerData);
      //this.dataToUpdate(growerData);
      

    } else {
      
    }
  }

  onSubmit(formValue: any) {
    
    const id = formValue.id;
    const info = formValue.name + "-" + formValue.lastname1 + "-" + formValue.lastname2 + "-" + formValue.provincia
      + "-" + formValue.canton + "-" + formValue.distrito + "-" + formValue.birthday + "-1-2-" + formValue.deliveradress + "-" + formValue.deliveradress;
    this.growerService.addGrower(id, info);

  }

  dataToUpdate(growerData) {
    this.growerform = this.formB.group({
      name: growerData.nombre,
      lastname1: growerData.apellido1,
      lastname2: growerData.apellido2,
      id: growerData.cedula.toString(),
      phoneNumber: growerData.telefono,
      simpenumber: growerData.sinpe,
      birthday: '',
      deliveradress: '',
      provincia: growerData.direccion[0],
      canton: growerData.direccion[1],
      distrito: growerData.direccion[2]
    })

  }

}     

