import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  Response,
  HttpCode,
} from '@nestjs/common';
import { AutorsService } from './autors.service';
import { CreateAutorDto } from './dto/create-autor.dto';
import { UpdateAutorDto } from './dto/update-autor.dto';

@Controller('authors')
export class AutorsController {
  lock = {};
  constructor(private readonly autorsService: AutorsService) {}

  @Post()
  @HttpCode(201)
  async create(@Body() createAutorDto: CreateAutorDto) {
    console.log('Create: Autors');
    console.log(createAutorDto);
    return await this.autorsService.create(createAutorDto);
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    console.log('Find: Autors');
    console.log(id);
    return this.autorsService.findOne(id);
  }

  @Patch(':id')
  async update(
    @Param('id') id: string,
    @Body() updateAutorDto: UpdateAutorDto,
  ) {
    console.log('Update: Autors');
    console.log(id);
    console.log(updateAutorDto);
    this.lock[id] = true;
    const response = await this.autorsService.update(id, updateAutorDto);
    delete this.lock[id];
    return response;
  }

  @Delete(':id')
  @HttpCode(204)
  remove(@Param('id') id: string) {
    console.log('Remove: Autors');
    console.log(id);
    return this.autorsService.remove(id);
  }
}
