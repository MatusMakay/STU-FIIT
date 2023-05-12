import { Module } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';
import { AppController } from './app.controller';
import { DatabaseService } from './database.service';
import { PostgresModule } from './postgres/postgres.module';

@Module({
  imports: [PostgresModule, ConfigModule.forRoot()],
  controllers: [AppController],
  providers: [DatabaseService],
})


export class AppModule { }
