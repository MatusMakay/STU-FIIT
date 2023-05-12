import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
  Patch,
  Post,
  HttpCode,
  HttpException,
} from '@nestjs/common';
import { CreateCardsDto } from './dto/create-cards.dto';
import { UpdateCardsDto } from './dto/update-cards.dto';
import { CardsService } from './cards.service';

@Controller('cards')
export class CardsController {
  constructor(private readonly cardsService: CardsService) {}

  @Post()
  @HttpCode(201)
  async create(@Body() createCardsDto: CreateCardsDto) {
    console.log('Create: Cards');
    console.log(createCardsDto);
    if (!createCardsDto) {
      throw new HttpException('', 400);
    }
    return await this.cardsService.create(createCardsDto);
  }

  @Get(':id')
  async findOne(@Param('id') id: string) {
    console.log('Find: Cards');
    console.log(id);
    return await this.cardsService.findOne(id);
  }

  @Patch(':id')
  @HttpCode(200)
  async update(@Param('id') id: string, @Body() updateCardDto: UpdateCardsDto) {
    console.log('Update: Cards');
    console.log(id);
    console.log(updateCardDto);

    return await this.cardsService.update(id, updateCardDto);
  }

  @Delete(':id')
  @HttpCode(204)
  async delete(@Param('id') id: string) {
    console.log('Remove: Cards');
    console.log(id);
    if (id != null) return await this.cardsService.remove(id);
    else throw new HttpException('', 400);
  }
}
