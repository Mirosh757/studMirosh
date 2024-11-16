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
            $table->date('date_birth')->nullable(false);
            $table->date('date_admission')->nullable(true);
            //FK
            $table->unsignedbigInteger('speciality_id');
            $table->foreign('speciality_id')->references('id')->on('specialities');

            $table->unsignedbigInteger('doctor_id');
            $table->foreign('doctor_id')->references('id')->on('doctors');

            $table->unsignedbigInteger('department_id');
            $table->foreign('department_id')->references('id')->on('departments');
        });
        //Проверка, что дата начала работы меньше даты ее завершения
        DB::statement("ALTER TABLE doctor_specialities ADD CONSTRAINT valid_date_admission CHECK (date_admission <> NULL AND date_admission > date_birth OR date_admission = NULL)");
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
