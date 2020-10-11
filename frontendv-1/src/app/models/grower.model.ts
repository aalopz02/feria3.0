export class Grower {
  name: string;
  id: string;
  lastname: string;
  lastname2: string;
  phone_num: string;
  simpe_num: string;
  birth: string;
  adress: string;
  deliverplace: string;


  constructor(name: string, id: string, lastname: string, lastname2: string, phone_num: string, simpe_num: string, birth: string, adress: string, deliverplace: string){
    this.name = name;
    this.id = id;
    this.lastname = lastname;
    this.lastname2 = lastname2;
    this.phone_num = phone_num;
    this.simpe_num = simpe_num;
    this.birth = birth;
    this.adress = adress;
    this.deliverplace = deliverplace;
  }

}
