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
import { CopyService } from './copy.service';
import { CreateCopyDto } from './dto/create-copy.dto';
import { UpdateCopyDto } from './dto/update-copy.dto';
import { ResponseCopyDto } from './dto/response-copy.dto';

@Controller('instances')
export class CopyController {
  constructor(private readonly copyService: CopyService) {}

  @Post()
  @HttpCode(201)
  async create(@Body() createCopyDto: CreateCopyDto) {
    console.log('Create: Copy');
    console.log(createCopyDto);
    return await this.copyService.create(createCopyDto);
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    console.log('Find: Copy');
    console.log(id);
    return this.copyService.findOne(id);
  }

  @Patch(':id')
  update(@Param('id') id: string, @Body() updateCopyDto: UpdateCopyDto) {
    console.log('Update: Copy');
    console.log(id);
    console.log(updateCopyDto);
    return this.copyService.update(id, updateCopyDto);
  }

  @Delete(':id')
  @HttpCode(204)
  remove(@Param('id') id: string) {
    console.log('Remove: Copy');
    console.log(id);
    return this.copyService.remove(id);
  }
}
