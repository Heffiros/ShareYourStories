/*
  Warnings:

  - You are about to drop the column `Content` on the `Page` table. All the data in the column will be lost.
  - Added the required column `content` to the `Page` table without a default value. This is not possible if the table is not empty.

*/
-- AlterTable
ALTER TABLE `Page` DROP COLUMN `Content`,
    ADD COLUMN `content` VARCHAR(191) NOT NULL;
