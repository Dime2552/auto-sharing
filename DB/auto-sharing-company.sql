-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema auto-sharing-company
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema auto-sharing-company
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `auto-sharing-company` DEFAULT CHARACTER SET utf8 ;
USE `auto-sharing-company` ;

-- -----------------------------------------------------
-- Table `auto-sharing-company`.`CarStatuses`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `auto-sharing-company`.`CarStatuses` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `status` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `auto-sharing-company`.`Cars`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `auto-sharing-company`.`Cars` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `licence_plate` VARCHAR(45) NOT NULL,
  `model` VARCHAR(45) NULL,
  `year` VARCHAR(45) NULL,
  `status_id` INT NOT NULL,
  PRIMARY KEY (`id`, `status_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  UNIQUE INDEX `licence_plate_UNIQUE` (`licence_plate` ASC) VISIBLE,
  INDEX `fk_Car_Status_idx` (`status_id` ASC) VISIBLE,
  CONSTRAINT `fk_Car_Status`
    FOREIGN KEY (`status_id`)
    REFERENCES `auto-sharing-company`.`CarStatuses` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `auto-sharing-company`.`ReservationStatuses`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `auto-sharing-company`.`ReservationStatuses` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `status` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `auto-sharing-company`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `auto-sharing-company`.`Users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `phone_number` VARCHAR(45) NULL,
  `email` VARCHAR(45) NULL,
  `licence_number` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  UNIQUE INDEX `licence_number_UNIQUE` (`licence_number` ASC) VISIBLE,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `auto-sharing-company`.`Reservations`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `auto-sharing-company`.`Reservations` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `cars_id` INT NOT NULL,
  `status_id` INT NOT NULL,
  `start_time` DATETIME NULL,
  `end_time` DATETIME NULL,
  `Users_id` INT NOT NULL,
  PRIMARY KEY (`id`, `status_id`, `cars_id`, `Users_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_Reservations_Cars1_idx` (`cars_id` ASC) VISIBLE,
  INDEX `fk_Reservations_ReservationStatuses1_idx` (`status_id` ASC) VISIBLE,
  INDEX `fk_Reservations_Users1_idx` (`Users_id` ASC) VISIBLE,
  CONSTRAINT `fk_Reservations_Cars1`
    FOREIGN KEY (`cars_id`)
    REFERENCES `auto-sharing-company`.`Cars` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reservations_ReservationStatuses1`
    FOREIGN KEY (`status_id`)
    REFERENCES `auto-sharing-company`.`ReservationStatuses` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reservations_Users1`
    FOREIGN KEY (`Users_id`)
    REFERENCES `auto-sharing-company`.`Users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
