import { Component, OnInit } from '@angular/core';
import { Grower } from '../../../models/grower.model';
import { GrowerService } from '../../../services/grower.service'
import { FormBuilder, FormControl, FormGroup, FormArray } from '@angular/forms'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  growers: Grower[];
  growerform: FormGroup;
  
  constructor(private formB: FormBuilder, private growerService: GrowerService) {
    this.growers = [];
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
      adress: ['']
    })
  }

  onSubmit(formValue: any) {
    
    const id = formValue.id;
    const info = formValue.name + "-" + formValue.lastname1 + "-" + formValue.lastname2 + "-" + "cartago"
      + "-" + "azucar" + "-" + formValue.adress + "-" + formValue.birthday + "-1-2-" + formValue.deliveradress + "-" + formValue.deliveradress;
    this.growerService.addGrower(id, info);

  }

  addGrower(name, lastname1, lastname2, id, phoneNumber, simpenumber, birthday, deliveradress, adress){
    
    this.growers.push(new Grower(name.value, id.value, lastname1.value, lastname2.value, phoneNumber.value, simpenumber.value, birthday.value, adress.value, deliveradress.value));

    for (let elemento of this.growers) {
      console.log(elemento);
    }
  }
}     

