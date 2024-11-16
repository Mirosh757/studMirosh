<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateGeneralPagesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('general_pages', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->string('title', 250)->nullable(false);
            $table->string('website', 30)->nullable(false);
            
        });
        //Проверка на валидное название
        DB::statement("ALTER TABLE general_pages ADD CONSTRAINT valid_title CHECK (trim(title) <> '' AND title ~* '^[А-Яа-я0-9.,-<> №()]{7,}$')");

        //Проверка на валидный веб сайт
        DB::statement("ALTER TABLE general_pages ADD CONSTRAINT valid_website CHECK (trim(website) <> '' AND website ~* '^[A-Za-z0-9._%+-:/]{5,}$')");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('general_pages');
    }
}
