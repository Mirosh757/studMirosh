<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateSpecialitiesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('specialities', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->string('title', 16)->unique()->nullable(false);
        });
        //Проверка, что профессия не состоит из одних пробелов
        DB::statement("ALTER TABLE specialities ADD CONSTRAINT valid_title CHECK (trim(title) <> '' AND title ~* '^[^\\s]+.*$')");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('specialities');
    }
}
