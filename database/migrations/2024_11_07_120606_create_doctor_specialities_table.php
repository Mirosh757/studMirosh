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
<<<<<<< HEAD
            $table->date('date_end')->nullable(false);
=======
            $table->date('date_end')->nullable(true);
>>>>>>> 1c1bbd42265a521a3a00f48fb891dca16d8de4b1
            //FK
            $table->unsignedbigInteger('speciality_id');
            $table->foreign('speciality_id')->references('id')->on('specialities')->cascadeOnUpdate()->cascadeOnDelete();

            $table->unsignedbigInteger('doctor_id');
            $table->foreign('doctor_id')->references('id')->on('doctors')->cascadeOnUpdate()->cascadeOnDelete();

            $table->unsignedbigInteger('department_id');
            $table->foreign('department_id')->references('id')->on('departments')->cascadeOnUpdate()->cascadeOnDelete();
        });
        //Проверка, что дата начала работы меньше даты ее завершения
        DB::statement("ALTER TABLE doctor_specialities ADD CONSTRAINT valid_date_end CHECK (date_start <= COALESCE(date_end, CURRENT_DATE) AND date_start <= CURRENT_DATE AND date_end <= CURRENT_DATE)");
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
