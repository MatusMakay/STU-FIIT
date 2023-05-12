import { Module } from '@nestjs/common';
import { AutorsService } from './autors.service';
import { AutorsController } from './autors.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Autors } from './entities/autor.entity';

@Module({
  imports: [TypeOrmModule.forFeature([
    Autors,
  ])],
  controllers: [AutorsController],
  providers: [AutorsService]
})
export class AutorsModule {}
