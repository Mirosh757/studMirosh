<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateRequisitesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('requisites', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->date('registration_date')->unique()->nullable(false);
            $table->string('hospital_reduce_name', 75)->unique()->nullable(false);
            $table->string('name_legal_faces', 50)->unique()->nullable(false);
            $table->string('ogrn', 13)->unique()->nullable(false);
            $table->string('inn', 12)->unique()->nullable(false);
            $table->string('kpp', 9)->unique()->nullable(false);
            //FK
            $table->unsignedbigInteger('hospital_id');
            $table->foreign('hospital_id')->references('id')->on('hospitals');
        });
        //Проверка на валидное, сокращенное название больницы
        DB::statement("ALTER TABLE requisites ADD CONSTRAINT valid_hospital_reduce_name CHECK (trim(hospital_reduce_name) <> '' AND hospital_reduce_name ~* '^[А-Яа-я0-9.,№ ]{5,}$')");
        
        //Проверка на валидное имя представителя больницы
        DB::statement("ALTER TABLE requisites ADD CONSTRAINT valid_name_legal_faces CHECK (trim(name_legal_faces) <> '' AND name_legal_faces ~* '^[А-Яа-я., ]{4,}$')");
        
        //Проверка на валидное OGRN
        DB::statement("ALTER TABLE requisites ADD CONSTRAINT valid_ogrn CHECK (trim(ogrn) <> '' AND ogrn ~* '^[0-9]{13}$')");
        
        //Проверка на валидное INN
        DB::statement("ALTER TABLE requisites ADD CONSTRAINT valid_inn CHECK (trim(inn) <> '' AND inn ~* '^[0-9]{12}$')");
        
        //Проверка на валидное KPP
        DB::statement("ALTER TABLE requisites ADD CONSTRAINT valid_kpp CHECK (trim(kpp) <> '' AND kpp ~* '^[0-9]{9}$')");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('requisites');
    }
}
