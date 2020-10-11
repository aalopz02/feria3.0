import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GrowerService } from '../../../services/grower.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {

  updateGrowerform: FormGroup;

  constructor(private formB: FormBuilder, private growerService: GrowerService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {

    this.updateGrowerform = this.formB.group({
      id: [''],
    })

  }

  onSubmit(formValue: any) {
    this.router.navigate(['admin/p-update/', formValue.id]);

    console.log(formValue.id);
  }


}
