use MedClinic
delete from Properties

insert into Properties(id,name)
values (newid(),'Вес'),
(newid(),'Рост'),
(newid(),'Анализ крови'),
(newid(),'Анализ мочи'),
(newid(),'ЭКГ'),
(newid(),'Анализ кожных покровов'),
(newid(),'Анализ волосяных покровов'),
(newid(),'Анализ печени')