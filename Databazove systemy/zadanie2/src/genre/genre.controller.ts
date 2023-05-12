import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  HttpCode,
} from '@nestjs/common';
import { GenreService } from './genre.service';
import { CreateGenreDto } from './dto/create-genre.dto';
import { UpdateGenreDto } from './dto/update-genre.dto';

@Controller('categories')
export class GenreController {
  lock = [];
  constructor(private readonly genreService: GenreService) {}

  removeItemOnce(arr, value) {
    var index = arr.indexOf(value);
    if (index > -1) {
      arr.splice(index, 1);
    }
    return arr;
  }

  @Post()
  @HttpCode(201)
  async create(@Body() createGenreDto: CreateGenreDto) {
    console.log('Create: Genre');
    console.log(createGenreDto);
    return await this.genreService.create(createGenreDto);
  }

  @Get(':id')
  async findOne(@Param('id') id: string) {
    console.log('Find: Genre');
    console.log(id);
    return this.genreService.findOne(id);
  }

  @Patch(':id')
  async update(
    @Param('id') id: string,
    @Body() updateGenreDto: UpdateGenreDto,
  ) {
    console.log('Update: Genre');
    console.log(id);
    console.log(updateGenreDto);
    const response = await this.genreService.update(id, updateGenreDto);
    console.log('updated it');
    return response;
  }

  @Delete(':id')
  @HttpCode(204)
  async remove(@Param('id') id: string) {
    console.log('Remove: Genre');
    console.log(id);
    await this.genreService.remove(id);
    console.log('REMOVE IT');
    return [];
  }
}
