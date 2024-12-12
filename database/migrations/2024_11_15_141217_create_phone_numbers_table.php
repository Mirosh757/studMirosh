<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreatePhoneNumbersTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('phone_numbers', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->string('contact_phone_number', 11)->nullable(false);
            //FK
            $table->unsignedbigInteger('general_page_id');
            $table->foreign('general_page_id')->references('id')->on('general_pages')->cascadeOnUpdate()->cascadeOnDelete();
        });
        // Проверка на валидный телефон (11 цифр)
        DB::statement("ALTER TABLE phone_numbers ADD CONSTRAINT valid_contact_phone_number CHECK (contact_phone_number ~ '^[0-9]{11}$')");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('phone_numbers');
    }
}
