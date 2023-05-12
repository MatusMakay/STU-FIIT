import { DataSource, DataSourceOptions, Migration } from "typeorm";

export const dataSourceOptions: DataSourceOptions = {
    type: 'postgres',
    database: 'db',
    username: 'postgres',
    password: 'Hello',
    synchronize: true,
    entities: ['dist/**/*.entity.js'],
    migrations: ['dist/db/migrations/*.js']
}

const dataSource = new DataSource(dataSourceOptions);
export default dataSource;