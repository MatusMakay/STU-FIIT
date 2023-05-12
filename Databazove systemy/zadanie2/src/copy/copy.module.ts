import { Module } from '@nestjs/common';
import { CopyService } from './copy.service';
import { CopyController } from './copy.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Copy } from './entities/copy.entity';
import { Publications } from 'src/publication/entities/publication.entity';

@Module({
  imports: [TypeOrmModule.forFeature([
    Copy,
    Publications
  ])],
  controllers: [CopyController],
  providers: [CopyService]
})
export class CopyModule {}
