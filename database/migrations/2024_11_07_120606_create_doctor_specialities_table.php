<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateDoctorSpecialitiesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('doctor_specialities', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->date('date_start')->nullable(false);
            $table->date('date_end')->default('2024-11-23');
            //FK
            $table->unsignedbigInteger('speciality_id');
            $table->foreign('speciality_id')->references('id')->on('specialities');

            $table->unsignedbigInteger('doctor_id');
            $table->foreign('doctor_id')->references('id')->on('doctors');

            $table->unsignedbigInteger('department_id');
            $table->foreign('department_id')->references('id')->on('departments');
        });
        //Проверка, что дата начала работы меньше даты ее завершения
        DB::statement("ALTER TABLE doctor_specialities ADD CONSTRAINT valid_date_end CHECK (date_end > date_start)");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('doctor_specialities');
    }
}
