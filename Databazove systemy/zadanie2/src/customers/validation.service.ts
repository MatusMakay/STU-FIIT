import { Injectable } from "@nestjs/common";

@Injectable()
export class ValidateService {
    
    emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    emailValidation(email: string){
        if(email.match(this.emailRegex)){
            return true;
        }

        return false;
    }

}