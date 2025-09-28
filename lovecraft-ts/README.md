# SYS

## Pré requis

- Yarn (1.22)
- PostgreSQL => Install DockerContainer
- Node (22)

## Installation

Install depedencies

```bash
yarn
```

Setup Env

```bash
cp .env.example .env
```

Fill .env

Apply Prisma Migrations

```bash
npx prisma migrate deploy
```

## Usage

```bash
yarn dev
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
