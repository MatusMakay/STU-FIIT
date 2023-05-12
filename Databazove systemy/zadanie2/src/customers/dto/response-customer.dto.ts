import { CreateRentalsDto } from "src/rentals/dto/create-rentals.dto";
import { CreateReservationsDto } from "src/reservations/dto/create-reservations.dto";

export class ResponseCustomerDto {
     id :  string;
     name :  string;  
     surname :  string;  
     email :  string;  
     birth_date :  Date;  
     personal_identificator :  string;  
     reservations : Array<CreateReservationsDto | Object>;
     rentals : Array<CreateRentalsDto| Object>;
     created_at :  Date;  
     updated_at :  Date;
}
