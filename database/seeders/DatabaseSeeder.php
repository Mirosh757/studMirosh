<?php
namespace Database\Seeders;
use Illuminate\Database\Seeder;
use App\Models\phone_number;
use App\Models\department;
use App\Models\doctor_speciality;
use App\Models\doctor;
use App\Models\general_page;
use App\Models\hospital;
use App\Models\region;
use App\Models\requisites;
use App\Models\speciality;
use App\Models\address;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     * 
     * @return void
     */
    public function run()
    {
        $region1=region::create(array(
            'region_name'=>'Улан-Удэ'
        ));
        $region2=region::create(array(
            'region_name'=>'Москва'
        ));
        $region3=region::create(array(
            'region_name'=>'Санкт-Петербург'
        ));
        $region4=region::create(array(
            'region_name'=>'Екатеринбург'
        ));
        $region5=region::create(array(
            'region_name'=>'Калининград'
        ));
        $region6=region::create(array(
            'region_name'=>'Курск'
        ));
        $region7=region::create(array(
            'region_name'=>'Казань'
        ));
        $region8=region::create(array(
            'region_name'=>'Омск'
        ));
        $region9=region::create(array(
            'region_name'=>'Томск'
        ));
        $region10=region::create(array(
            'region_name'=>'Новосибирск'
        ));
        $general_page1=general_page::create(array(
            'title'=>'Государственное автономное учреждение здравоохранения <<Республиканская клиническая больница им. Н.А.Семашко>>',
            'website'=>'http://www.rkbsemashko.ru/'
        ));
        $general_page2=general_page::create(array(
            'title'=>'Государственное автономное учреждение здравоохранения <<Детская республиканская клиническая больница>>',
            'website'=>'http://drkbrb.ru/'
        ));
        $general_page3=general_page::create(array(
            'title'=>'ГАУЗ <<Республиканская клиническая больница скорой медицинской помощи им. В.В. Ангапова>> г. Улан-Удэ',
            'website'=>'http://bsmp03.ru/'
        ));
        $general_page4=general_page::create(array(
            'title'=>'Государственное бюджетное учреждение здравоохранения <<Городская больница 2>>',
            'website'=>'https://xn--2-btbfp1ai/'
        ));
        $general_page5=general_page::create(array(
            'title'=>'Государственное бюджетное учреждение здравоохранения <<Городская больница 3>>',
            'website'=>'http://gp3uu.ru/'
        ));
        $general_page6=general_page::create(array(
            'title'=>'Государственное бюджетное учреждение здравоохранения города Москвы <<Городская клиническая больница 13 Департамента здравоохранения города Москвы>>',
            'website'=>'https://gkb13.ru/'
        ));
        $general_page7=general_page::create(array(
            'title'=>'УНИВЕРСИТЕТСКАЯ КЛИНИЧЕСКАЯ БОЛЬНИЦА ИМЕНИ В.В.ВИНОГРАДОВА (ФИЛИАЛ) <<РОССИЙСКИЙ УНИВЕРСИТЕТ ДРУЖБЫ НАРОДОВ ИМЕНИ ПАТРИСА ЛУМУМБЫ>>',
            'website'=>'https://gkb64.ru/'
        ));
        $general_page8=general_page::create(array(
            'title'=>'Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская Мариинская больница>>',
            'website'=>'https://mariin.ru/'
        ));
        $general_page9=general_page::create(array(
            'title'=>'Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская клиническая больница 31>>',
            'website'=>'https://www.spbsverdlovka.ru/'
        ));
        $general_page10=general_page::create(array(
            'title'=>'Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Клиническая больница Святителя Луки>>',
            'website'=>'https://lucaclinic.ru/'
        ));
        $general_page11=general_page::create(array(
            'title'=>'Хирургическое',
            'website'=>'http://www.rkbsemashko.ru/'
        ));
        $general_page12=general_page::create(array(
            'title'=>'Педиатрическое',
            'website'=>'http://www.rkbsemashko.ru/'
        ));
        $general_page13=general_page::create(array(
            'title'=>'Терапевтическое',
            'website'=>'http://www.rkbsemashko.ru/'
        ));
        $general_page14=general_page::create(array(
            'title'=>'Хирургическое',
            'website'=>'http://drkbrb.ru/'
        ));
        $general_page15=general_page::create(array(
            'title'=>'Неврологическое',
            'website'=>'http://drkbrb.ru/'
        ));
        $general_page16=general_page::create(array(
            'title'=>'Нефрологическое',
            'website'=>'http://bsmp03.ru/'
        ));
        $general_page17=general_page::create(array(
            'title'=>'Кардиологическое',
            'website'=>'http://bsmp03.ru/'
        ));
        $general_page18=general_page::create(array(
            'title'=>'Гинекологическое',
            'website'=>'https://lucaclinic.ru/'
        ));
        $general_page19=general_page::create(array(
            'title'=>'Психиотрическое',
            'website'=>'https://lucaclinic.ru/'
        ));
        $general_page20=general_page::create(array(
            'title'=>'Терапевтическое',
            'website'=>'https://www.spbsverdlovka.ru/'
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012437005',
            'general_page_id'=>$general_page1->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012373040',
            'general_page_id'=>$general_page2->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012556252',
            'general_page_id'=>$general_page3->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012233524',
            'general_page_id'=>$general_page4->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012437043',
            'general_page_id'=>$general_page5->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012437004',
            'general_page_id'=>$general_page6->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012373042',
            'general_page_id'=>$general_page7->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012556256',
            'general_page_id'=>$general_page8->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012233520',
            'general_page_id'=>$general_page9->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012437044',
            'general_page_id'=>$general_page10->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012232027',
            'general_page_id'=>$general_page11->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012451898',
            'general_page_id'=>$general_page12->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012437005',
            'general_page_id'=>$general_page13->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012412543',
            'general_page_id'=>$general_page14->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012437875',
            'general_page_id'=>$general_page15->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012232026',
            'general_page_id'=>$general_page16->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012451897',
            'general_page_id'=>$general_page17->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012437000',
            'general_page_id'=>$general_page18->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012412544',
            'general_page_id'=>$general_page19->id
        ));
        phone_number::create(array(
            'contact_phone_number'=>'83012437872',
            'general_page_id'=>$general_page20->id
        ));
        address::create(array(
            'address'=>'корп. 2, ул. Пирогова, 3а, Улан-Удэ, Респ. Бурятия, 670047',
            'general_page_id'=>$general_page1->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 2А, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page2->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page3->id
        ));
        address::create(array(
            'address'=>'ул. Павлова, 12, Улан-Удэ, Респ. Бурятия, 670031',
            'general_page_id'=>$general_page4->id
        ));
        address::create(array(
            'address'=>'ул. Павлова, 2а, Улан-Удэ, Респ. Бурятия, 670031',
            'general_page_id'=>$general_page5->id
        ));
        address::create(array(
            'address'=>'670004, Республика Бурятия, город Улан-Удэ, п. Стеклозавод, улица Воронежская 1а',
            'general_page_id'=>$general_page6->id
        ));
        address::create(array(
            'address'=>'670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.155',
            'general_page_id'=>$general_page7->id
        ));
        address::create(array(
            'address'=>'г. Москва  ул.Велозаводская, д 1 / 1',
            'general_page_id'=>$general_page8->id
        ));
        address::create(array(
            'address'=>'ул. Вавилова, 61 строение 11, Москва, 117292',
            'general_page_id'=>$general_page9->id
        ));
        address::create(array(
            'address'=>'Литейный пр., 56, Санкт-Петербург, 191014',
            'general_page_id'=>$general_page10->id
        ));
        address::create(array(
            'address'=>'пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110',
            'general_page_id'=>$general_page11->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page12->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page13->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page14->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page15->id
        ));
        address::create(array(
            'address'=>'Улан-Удэ, Респ. Бурятия, 670031',
            'general_page_id'=>$general_page16->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page17->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page18->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page19->id
        ));
        address::create(array(
            'address'=>'пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042',
            'general_page_id'=>$general_page20->id
        ));
        $hospital1=hospital::create(array(
            'id'=>$general_page1->id,
            'region_id'=>$region1->id
        ));
        $hospital2=hospital::create(array(
            'id'=>$general_page2->id,
            'region_id'=>$region1->id
        ));
        $hospital3=hospital::create(array(
            'id'=>$general_page3->id,
            'region_id'=>$region1->id
        ));
        $hospital4=hospital::create(array(
            'id'=>$general_page4->id,
            'region_id'=>$region2->id
        ));
        $hospital5=hospital::create(array(
            'id'=>$general_page5->id,
            'region_id'=>$region3->id
        ));
        $hospital6=hospital::create(array(
            'id'=>$general_page6->id,
            'region_id'=>$region4->id
        ));
        $hospital7=hospital::create(array(
            'id'=>$general_page7->id,
            'region_id'=>$region5->id
        ));
        $hospital8=hospital::create(array(
            'id'=>$general_page8->id,
            'region_id'=>$region5->id
        ));
        $hospital9=hospital::create(array(
            'id'=>$general_page9->id,
            'region_id'=>$region6->id
        ));
        $hospital10=hospital::create(array(
            'id'=>$general_page10->id,
            'region_id'=>$region6->id
        ));
        requisites::create(array(
            'registration_date'=>'12-03-1965',
            'hospital_reduce_name'=>'РКБ им. Н.А.Семашко',
            'name_legal_faces'=>'Овечкин В.А.',
            'ogrn'=>'1122315577445',
            'inn'=>'463248513644',
            'kpp'=>'642478976',
            'hospital_id'=>$hospital1->id
        ));
        requisites::create(array(
            'registration_date'=>'19-11-1987',
            'hospital_reduce_name'=>'ДРКБ ',
            'name_legal_faces'=>'Оверин В.А.',
            'ogrn'=>'1122315571445',
            'inn'=>'465136443248',
            'kpp'=>'478976642',
            'hospital_id'=>$hospital2->id
        ));
        requisites::create(array(
            'registration_date'=>'01-02-1955',
            'hospital_reduce_name'=>'ГАУЗ городская больница №1',
            'name_legal_faces'=>'Иванов И.И.',
            'ogrn'=>'1155771223445',
            'inn'=>'485134632644',
            'kpp'=>'478642976',
            'hospital_id'=>$hospital3->id
        ));
        requisites::create(array(
            'registration_date'=>'29-02-2000',
            'hospital_reduce_name'=>'ГБУЗ городская больница №2',
            'name_legal_faces'=>'Нечкин В.Ф',
            'ogrn'=>'1122315577415',
            'inn'=>'463248513641',
            'kpp'=>'786424976',
            'hospital_id'=>$hospital4->id
        ));
        requisites::create(array(
            'registration_date'=>'11-02-1879',
            'hospital_reduce_name'=>'ГБУЗ городская больница №3',
            'name_legal_faces'=>'Добров Е.П',
            'ogrn'=>'2112377441555',
            'inn'=>'851463243644',
            'kpp'=>'649762478',
            'hospital_id'=>$hospital5->id
        ));
        requisites::create(array(
            'registration_date'=>'30-12-1965',
            'hospital_reduce_name'=>'УКБ им В.В.Виноградова',
            'name_legal_faces'=>'Петров Д.Н.',
            'ogrn'=>'1122531557744',
            'inn'=>'465133248644',
            'kpp'=>'789766424',
            'hospital_id'=>$hospital6->id
        ));
        requisites::create(array(
            'registration_date'=>'12-09-1977',
            'hospital_reduce_name'=>'ГБУЗ города Москвы городская больница №13',
            'name_legal_faces'=>'Синицин Н.Д.',
            'ogrn'=>'1155774451223',
            'inn'=>'463213644854',
            'kpp'=>'642894776',
            'hospital_id'=>$hospital7->id
        ));
        requisites::create(array(
            'registration_date'=>'19-11-1695',
            'hospital_reduce_name'=>'ГБУЗ городская больница',
            'name_legal_faces'=>'Иванов А.А.',
            'ogrn'=>'1182315577445',
            'inn'=>'463245136448',
            'kpp'=>'642976478',
            'hospital_id'=>$hospital8->id
        ));
        requisites::create(array(
            'registration_date'=>'17-06-1888',
            'hospital_reduce_name'=>'ГБУЗ городская больница №31',
            'name_legal_faces'=>'Сидоров В.П.',
            'ogrn'=>'5112744523157',
            'inn'=>'513644463248',
            'kpp'=>'678974246',
            'hospital_id'=>$hospital9->id
        ));
        requisites::create(array(
            'registration_date'=>'16-10-1811',
            'hospital_reduce_name'=>'ГБУЗ Клиническая больница Святителя Луки',
            'name_legal_faces'=>'Багданов К.В.',
            'ogrn'=>'1125577423145',
            'inn'=>'463213644485',
            'kpp'=>'648976247',
            'hospital_id'=>$hospital10->id
        ));
        $department1=department::create(array(
            'id'=>$general_page11->id,
            'hospital_id'=>$hospital1->id
        ));
        $department2=department::create(array(
            'id'=>$general_page12->id,
            'hospital_id'=>$hospital1->id
        ));
        $department3=department::create(array(
            'id'=>$general_page13->id,
            'hospital_id'=>$hospital1->id
        ));
        $department4=department::create(array(
            'id'=>$general_page14->id,
            'hospital_id'=>$hospital2->id
        ));
        $department5=department::create(array(
            'id'=>$general_page15->id,
            'hospital_id'=>$hospital2->id
        ));
        $department6=department::create(array(
            'id'=>$general_page16->id,
            'hospital_id'=>$hospital3->id
        ));
        $department7=department::create(array(
            'id'=>$general_page17->id,
            'hospital_id'=>$hospital4->id
        ));
        $department8=department::create(array(
            'id'=>$general_page18->id,
            'hospital_id'=>$hospital4->id
        ));
        $department9=department::create(array(
            'id'=>$general_page19->id,
            'hospital_id'=>$hospital5->id
        ));
        $department10=department::create(array(
            'id'=>$general_page20->id,
            'hospital_id'=>$hospital6->id
        ));
        $doctor1=doctor::create(array(
            'name'=>'Георгий Васильевич Кондратов',
            'address'=>'ул. Революции 1905 года; 36; Улан-Удэ; Респ. Бурятия; 670034',
            'passport_details'=>'8123432544',
            'date_birth'=>'27-10-1954'
        ));
        $doctor2=doctor::create(array(
            'name'=>'Иван Афанасьевич Овчинников',
            'address'=>'ул. Комсомольская; 1Б; Улан-Удэ; Респ. Бурятия; 670002',
            'passport_details'=>'8133431234',
            'date_birth'=>'14-09-1922'
        ));
        $doctor3=doctor::create(array(
            'name'=>'Эрдэн Раднаевич Раднаев',
            'address'=>'ул. Октябрьская; 36а; Улан-Удэ; Респ. Бурятия; 670002',
            'passport_details'=>'7421532544',
            'date_birth'=>'27-02-1954'
        ));
        $doctor4=doctor::create(array(
            'name'=>'Валентин Николаевич Ильков',
            'address'=>'ул. Цивилева; 2; Улан-Удэ; Респ. Бурятия; 670034',
            'passport_details'=>'8823432544',
            'date_birth'=>'09-09-1909'
        ));
        $doctor5=doctor::create(array(
            'name'=>'Дмитрий Николаевич Плишкин',
            'address'=>'ул. Цивилева; д.41; Улан-Удэ; Респ. Бурятия; 670034',
            'passport_details'=>'8123432987',
            'date_birth'=>'22-12-1944'
        ));
        $doctor6=doctor::create(array(
            'name'=>'Александр Владимирович Алексеев',
            'address'=>'ул. Модогоева; 1/1; Улан-Удэ; Респ. Бурятия; 670000',
            'passport_details'=>'8155532544',
            'date_birth'=>'09-05-1945'
        ));
        $doctor7=doctor::create(array(
            'name'=>'Иннокентий Иннокентьевич Ботвин',
            'address'=>'ул. Ключевская; 45Б; Улан-Удэ; Респ. Бурятия; 670013',
            'passport_details'=>'8123432541',
            'date_birth'=>'08-05-1941'
        ));
        $doctor8=doctor::create(array(
            'name'=>'Петр Васильевич Васильев',
            'address'=>'ул. Каландаришвили; 27; Улан-Удэ; Респ. Бурятия; 670000',
            'passport_details'=>'1126432544',
            'date_birth'=>'21-12-1921'
        ));
        $doctor9=doctor::create(array(
            'name'=>'Герман Артемьевич Головач',
            'address'=>'ул. Юного Коммунара; 3; Улан-Удэ; Респ. Бурятия; 670000',
            'passport_details'=>'6447632544',
            'date_birth'=>'18-08-1912'
        ));
        $doctor10=doctor::create(array(
            'name'=>'Леонид Арвидович Гофланд',
            'address'=>'ул. Хоца Намсараева; 2Б; Улан-Удэ; Респ. Бурятия; 670034',
            'passport_details'=>'8123432456',
            'date_birth'=>'17-07-1933'
        ));
        $speciality1=speciality::create(array(
            'title'=>'хирург'
        ));
        $speciality2=speciality::create(array(
            'title'=>'терапевт'
        ));
        $speciality3=speciality::create(array(
            'title'=>'аллерголог'
        ));
        $speciality4=speciality::create(array(
            'title'=>'логопед'
        ));
        $speciality5=speciality::create(array(
            'title'=>'уролог'
        ));
        $speciality6=speciality::create(array(
            'title'=>'невролог'
        ));
        $speciality7=speciality::create(array(
            'title'=>'нефролог'
        ));
        $speciality8=speciality::create(array(
            'title'=>'кардиолог'
        ));
        $speciality9=speciality::create(array(
            'title'=>'педиатр'
        ));
        $speciality10=speciality::create(array(
            'title'=>'имунолог'
        ));
        doctor_speciality::create(array(
            'date_start'=>'1967-03-12',
            //'date_end'=>'2013-11-22',
            'speciality_id'=>$speciality1->id,
            'doctor_id'=>$doctor1->id,
            'department_id'=>$department1->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1955-12-17',
            'date_end'=>'2017-01-27',
            'speciality_id'=>$speciality1->id,
            'doctor_id'=>$doctor1->id,
            'department_id'=>$department1->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1967-08-27',
            'date_end'=>'2003-12-23',
            'speciality_id'=>$speciality2->id,
            'doctor_id'=>$doctor2->id,
            'department_id'=>$department1->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1971-01-10',
            'date_end'=>'2021-11-11',
            'speciality_id'=>$speciality3->id,
            'doctor_id'=>$doctor1->id,
            'department_id'=>$department2->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1988-02-28',
            'date_end'=>'2023-11-27',
            'speciality_id'=>$speciality4->id,
            'doctor_id'=>$doctor3->id,
            'department_id'=>$department3->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1934-07-17',
            'date_end'=>'1998-11-30',
            'speciality_id'=>$speciality5->id,
            'doctor_id'=>$doctor6->id,
            'department_id'=>$department4->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1923-03-12',
            'date_end'=>'1991-06-30',
            'speciality_id'=>$speciality6->id,
            'doctor_id'=>$doctor5->id,
            'department_id'=>$department4->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1955-12-22',
            'date_end'=>'2017-10-22',
            'speciality_id'=>$speciality7->id,
            'doctor_id'=>$doctor1->id,
            'department_id'=>$department5->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1961-01-31',
            'date_end'=>'2003-09-12',
            'speciality_id'=>$speciality8->id,
            'doctor_id'=>$doctor4->id,
            'department_id'=>$department5->id
        ));
        doctor_speciality::create(array(
            'date_start'=>'1969-08-22',
            'date_end'=>'2015-05-15',
            'speciality_id'=>$speciality9->id,
            'doctor_id'=>$doctor1->id,
            'department_id'=>$department6->id
        ));
    }
}