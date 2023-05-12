import { PartialType } from '@nestjs/mapped-types';
import { CreateCopyDto } from './create-copy.dto';


export class UpdateCopyDto extends PartialType(CreateCopyDto){
    type: string;
    publisher: string;
    year: number;
    status: string;
    publication_id: string;
}

