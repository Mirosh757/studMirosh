<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateDoctorsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('doctors', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->string('name', 50)->nullable(false);
            $table->string('address')->nullable(false);
            $table->string('passport_details', 11)->unique()->nullable(false);
            $table->date('date_birth')->nullable(false);
        });
        //Проверка, что имя не состоит из одних пробелов
        DB::statement("ALTER TABLE doctors ADD CONSTRAINT valid_name CHECK (trim(name) <> '' AND name ~* '^[^\\s]+.*$')");

        //Проверка на валидный адрес
        DB::statement("ALTER TABLE doctors ADD CONSTRAINT valid_address CHECK (trim(address) <> '' AND address ~* '^[А-Яа-я0-9.,-<> №() ]{7,}$')");

        //Проверка на валидные паспортные данные
        DB::statement("ALTER TABLE doctors ADD CONSTRAINT valid_passport_details CHECK (trim(passport_details) <> '' AND passport_details ~ '^[0-9]{10}$')");

        //Проверкак на валиждную дату рождения
        DB::statement("ALTER TABLE doctors ADD CONSTRAINT valid_date_birth CHECK (date_birth <= CURRENT_DATE AND date_birth >= '1880-01-01')");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('doctors');
    }
}
