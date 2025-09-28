# Étape 1 : build
FROM node:20-alpine AS build

WORKDIR /app

# Copier uniquement les fichiers nécessaires pour installer les deps
COPY package.json yarn.lock ./
COPY prisma/schema.prisma ./
# Installer dépendances
RUN yarn install --frozen-lockfile
RUN yarn prisma generate
# Copier le reste du code
COPY . .

# Compiler le TypeScript
RUN yarn build

# Étape 2 : run
FROM node:20-alpine AS runtime

WORKDIR /app

# Copier uniquement ce qui est nécessaire pour l’exécution
COPY --from=build /app/node_modules ./node_modules
COPY --from=build /app/dist ./dist
COPY package.json ./

# Exposer le port de Fastify
EXPOSE 3000

# Lancer l’API
CMD ["node", "dist/server.js"]
