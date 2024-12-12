<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateRegionsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('regions', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->string('region_name', 150)->nullable(false);
        });
        //Проверка на валидное имя региона
        DB::statement("ALTER TABLE regions ADD CONSTRAINT valid_region_name CHECK (trim(region_name) <> '' AND region_name ~* '^[А-Яа-я-]{2,}$')");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('regions');
    }
}
