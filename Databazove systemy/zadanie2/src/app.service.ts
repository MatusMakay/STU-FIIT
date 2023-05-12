import { Injectable } from '@nestjs/common';

@Injectable()
export class AppService {
  writeAutor(): string {
    return '<h1> Databazove systemy </h1> <h2> Made with love by Matus Makay </h2> ';
  }
}
