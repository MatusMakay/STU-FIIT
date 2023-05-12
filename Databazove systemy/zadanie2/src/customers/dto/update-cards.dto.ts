import { PartialType } from '@nestjs/mapped-types';
import { CreateCardsDto } from './create-cards.dto';

export class UpdateCardsDto extends PartialType(CreateCardsDto) {
    status: string;
    user_id: string;
}
