using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;


namespace ConsoleApp1
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        int HighHP(int hp);

    }

    [Table("characrer", Schema = "character")]
    public class Character
    {


        public int Id { get; set; }

        [Column("Name")]
        public String Name { get; set; }

        [Column("Wordlview")]
        public String Worldview { get; set; }

        [Column("Sex")]
        public String Sex { get; set; }

        [Column("Class")]
        public String Class { get; set; }

        [Column("Nation")]
        public String Nation { get; set; }
        public int level { get; set; }
        public int health { get; set; }
        public int speed { get; set; }
        public int experience { get; set; }

        public Character(String Name, String Worldview, String Sex, String Class, String Nation, int level, int health, int speed, int experience)
        {
            this.Name = Name;
            this.Worldview = Worldview;
            this.Sex = Sex;
            this.Class = Class;
            this.Nation = Nation;
            this.level = level;
            this.health = health;
            this.speed = speed;
            this.experience = experience;
        }
        public Character(int Id, String Name, String Worldview, String Sex, String Class, String Nation, int level, int health, int speed, int experience)
        {
            this.Id = Id;
            this.Name = Name;
            this.Worldview = Worldview;
            this.Sex = Sex;
            this.Class = Class;
            this.Nation = Nation;
            this.level = level;
            this.health = health;
            this.speed = speed;
            this.experience = experience; ;
        }

    }
        public class PostgreCharacterRepository : IRepository<Character>
        {
            string connectionString = "Host=localhost;Username=postgres;Password=Rbhf080166;Database=character";

            public IEnumerable<Character> GetAll()
            {
                using var con = new NpgsqlConnection(connectionString);
                con.Open();

                using var cmd = new NpgsqlCommand("SELECT * FROM character order by id_character", con);

                var reader = cmd.ExecuteReader();
                var result = new List<Character>();

                while (reader.Read())
                {
                    result.Add(new Character(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9)));
                }

                return result;
            }
            public void Create(Character character)
            {
                using var con = new NpgsqlConnection(connectionString);
                con.Open();

                using var cmd = new NpgsqlCommand($"INSERT INTO  character (name, worldview, sex, class, nation, level, health, speed, experience) VALUES (@name, @worldview, @sex, @class, @nation, @level, @health, @speed, @experience);", con);

                cmd.Parameters.Add(new NpgsqlParameter("name", character.Name));
                cmd.Parameters.Add(new NpgsqlParameter("worldview", character.Worldview));
                cmd.Parameters.Add(new NpgsqlParameter("sex", character.Sex));
                cmd.Parameters.Add(new NpgsqlParameter("class", character.Class));
                cmd.Parameters.Add(new NpgsqlParameter("nation", character.Nation));
                cmd.Parameters.Add(new NpgsqlParameter("level", character.level));
                cmd.Parameters.Add(new NpgsqlParameter("health", character.health));
                cmd.Parameters.Add(new NpgsqlParameter("speed", character.speed));
                cmd.Parameters.Add(new NpgsqlParameter("experience", character.experience));

                cmd.ExecuteNonQuery();
            }
            public void Update(Character character)
            {
                using var con = new NpgsqlConnection(connectionString);
                con.Open();

                using var cmd = new NpgsqlCommand($"UPDATE character SET name=@name, worldview=@worldview, sex=@sex, class=@class, nation=@nation, " +
                    $"level=@level, health=@health, speed=@speed, experience=@experience WHERE id_character={character.Id};", con);

                cmd.Parameters.Add(new NpgsqlParameter("name", character.Name));
                cmd.Parameters.Add(new NpgsqlParameter("worldview", character.Worldview));
                cmd.Parameters.Add(new NpgsqlParameter("sex", character.Sex));
                cmd.Parameters.Add(new NpgsqlParameter("class", character.Class));
                cmd.Parameters.Add(new NpgsqlParameter("nation", character.Nation));
                cmd.Parameters.Add(new NpgsqlParameter("level", character.level));
                cmd.Parameters.Add(new NpgsqlParameter("health", character.health));
                cmd.Parameters.Add(new NpgsqlParameter("speed", character.speed));
                cmd.Parameters.Add(new NpgsqlParameter("experience", character.experience));
                cmd.ExecuteNonQuery();
            }
            public void Delete(int id)
            {
                using var con = new NpgsqlConnection(connectionString);
                con.Open();

                using var cmd = new NpgsqlCommand($"DELETE FROM character WHERE id_character={id};", con);

                cmd.ExecuteNonQuery();
            }

            public int HighHP(int hp)
            {
                using var con = new NpgsqlConnection(connectionString);
                con.Open();

                using var cmd = new NpgsqlCommand($"select count (health)  from character where health={hp};", con);

                var reader = cmd.ExecuteReader();
                reader.Read();

                 return reader.GetInt32(0);

            }
            
        }
       


    
    class Program
    {
        static void Main(string[] args)
        {
            IRepository<Character> myRep = new PostgreCharacterRepository();
             
            char cmd;
            do
            {
                Console.WriteLine("\n\nq - Вывод списка\nw - Добавление новой строки\ne - Редактирование строки\nr - Удаление строки\nt - Ввод конкретного запроса\ny - Выход из программы");
                cmd = Console.ReadKey().KeyChar;
                switch (cmd)
                {
                    case 'q':
                        ShowCharacter(myRep);
                        break;
                    case 'w':
                        AddCharacter(myRep);
                        break;
                    case 'e':
                        UpdateCharacter(myRep);
                        break;
                    case 'r':
                        DeleteCharacter(myRep);
                        break;
                    case 't':
                        HPRequest(myRep);
                        break;
                    

                    case 'y':
                        Console.WriteLine("\n\n\nВыход из программы...");
                        break;
                }

            } while (cmd != 'y');
        }
        static void ShowCharacter(IRepository<Character> myRep)
        {
            var characterList = new List<Character>(myRep.GetAll());

            Console.WriteLine($"\nid\tname worldview sex\tclass\tnation\tlevel\thealth\tspeed\texperience");
            for (int i = 0; i < characterList.Count; i++)
                Console.WriteLine($"{characterList[i].Id}\t{characterList[i].Name} {characterList[i].Worldview}\t{characterList[i].Sex}\t{characterList[i].Class}\t{characterList[i].Nation}\t{characterList[i].level}\t{characterList[i].health}\t{characterList[i].speed}\t{characterList[i].experience}");
        }

        static void AddCharacter(IRepository<Character> myRep)
        {
            int level, health, speed, experience;
            string name, worldview, sex, _class, nation;


            Console.WriteLine("\nВведите имя героя");
            name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите мировоззрение героя");
            worldview = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите пол героя");
            sex = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите класс героя");
            _class = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите нацию героя");
            nation = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Введите уровень героя");
            level = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите здоровье героя");
            health = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите скорость героя");
            speed = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите уровень героя");
            experience = Convert.ToInt32(Console.ReadLine());

            Character character = new Character(name, worldview, sex, _class, nation, level, health, speed, experience);

            myRep.Create(character);
        }

        static void UpdateCharacter(IRepository<Character> myRep)
        {
            int level, health, speed, experience, id;
            string name, worldview, sex, _class, nation;

            Console.WriteLine("\nВведите ID ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите имя героя");
            name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите мировоззрение героя");
            worldview = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите пол героя");
            sex = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите класс героя");
            _class = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите нацию героя");
            nation = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите уровень героя");
            level = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите здоровье героя");
            health = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите скорость героя");
            speed = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите опыт героя");
            experience = Convert.ToInt32(Console.ReadLine());

            Character character = new Character(id, name, worldview, sex, _class, nation, level, health, speed, experience);

            myRep.Update(character);
        }
        static void DeleteCharacter(IRepository<Character> myRep)
        {
            int id;

            Console.WriteLine("\n Введите ID героя");
            id = Convert.ToInt32(Console.ReadLine());

            myRep.Delete(id);
        }


        static void HPRequest(IRepository<Character> myRep)
        {
            int hp = 0;
            int heroes;
            while(hp <= 0) { 
                Console.WriteLine("\nВведите HP:");
                hp = Convert.ToInt32(Console.ReadLine());
                if (hp <= 0) { 
                    Console.WriteLine("\nError: невозможное количество хп количество HP");
                }
                else {
                    heroes = myRep.HighHP(hp);
                    Console.WriteLine($"героев с HP = {hp}:   {heroes}");
                    }
            }
        }
    }
   
}
