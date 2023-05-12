import { Autors } from "src/autors/entities/autor.entity";
import { Genre } from "src/genre/entities/genre.entity";

export class ResponsePublicationDto{
    id :  string;
    title :  string; 
    authors : Array<Autors>;
    categories : Array<Genre>
    created_at :  Date;
    updated_at :  Date;
} 