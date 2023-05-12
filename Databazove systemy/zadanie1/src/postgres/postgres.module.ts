import { Module } from '@nestjs/common';
import { ConfigService, ConfigModule } from '@nestjs/config';
import { Pool } from 'pg'
@Module({
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
      envFilePath: `${process.cwd()}/environments/.env.${process.env.NODE_ENV}`,
    }),
  ],

  providers: [
    {
      provide: 'POSTGRES_DBS',
      useFactory: async (configService: ConfigService) => {
        const pool = new Pool({
          host: configService.get<String>('DATABASE_HOST'),
          port: configService.get<String>('DATABASE_PORT'),
          user: configService.get<String>('DATABASE_USER'),
          password: configService.get<String>('DATABASE_PASSWORD'),
          database: configService.get<String>('DATABASE_NAME'),
        });
        console.log(configService.get<String>('DATABASE_PASSWORD'))
        return pool;
      },
      inject: [ConfigService],
      // const pool = new Pool({
      //   host: process.env.DATABASE_HOST,
      //   port: process.env.DATABASE_PORT,
      //   user: process.env.DATABASE_USER,
      //   password: process.env.DATABASE_PASSWORD,
      //   database: process.env.DATABASE_NAME,
      // });
      //return pool;
    },
  ],
  exports: ['POSTGRES_DBS'],
})
export class PostgresModule { }


