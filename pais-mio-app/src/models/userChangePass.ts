export class UserChangePass {
    oldPass : string;
    newPass : string;
    newPassR : string;
    correo : string;
    isValid : boolean;  

    constructor(oldPass: string, newPass: string, newPassR: string, correo: string){
        this.oldPass = oldPass;
        this.newPass = newPass;
        this.newPassR = newPassR;
        this.correo = correo;
        this.isValid = false;
    }
}