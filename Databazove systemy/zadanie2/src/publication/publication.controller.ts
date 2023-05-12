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
import { PublicationService } from './publication.service';
import { CreatePublicationDto } from './dto/create-publication.dto';
import { UpdatePublicationDto } from './dto/update-publication.dto';

@Controller('publications')
export class PublicationController {
  constructor(private readonly publicationService: PublicationService) {}

  @Post()
  @HttpCode(201)
  create(@Body() createPublicationDto: CreatePublicationDto) {
    console.log('Publications: Create');
    console.log(createPublicationDto);
    return this.publicationService.create(createPublicationDto);
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    console.log('Publications: Find');
    console.log(id);
    return this.publicationService.findOne(id);
  }

  @Patch(':id')
  update(
    @Param('id') id: string,
    @Body() updatePublicationDto: UpdatePublicationDto,
  ) {
    console.log('Publications: Update');
    console.log(updatePublicationDto);
    return this.publicationService.update(id, updatePublicationDto);
  }

  @Delete(':id')
  @HttpCode(204)
  remove(@Param('id') id: string) {
    console.log('Publications: Remove');
    console.log(id);
    return this.publicationService.remove(id);
  }
}
