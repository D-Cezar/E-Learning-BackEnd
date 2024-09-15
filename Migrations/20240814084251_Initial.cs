using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Learning.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image_sourse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses", x => x.id);
                    table.ForeignKey(
                        name: "fk_courses_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_sections",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    video_source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    text_source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_sections", x => x.id);
                    table.ForeignKey(
                        name: "fk_course_sections_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "courses_users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    enroll_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    active_time = table.Column<TimeSpan>(type: "time", nullable: true),
                    completed_section_ids = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses_users", x => new { x.user_id, x.course_id });
                    table.ForeignKey(
                        name: "fk_courses_users_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_courses_users_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    section_course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_course_sections_section_course_id",
                        column: x => x.section_course_id,
                        principalTable: "course_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_answers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    given_answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answer_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_answers", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_answers_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_answers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "first_name", "last_name", "password", "role" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", "$2a$11$P7Fhr973nNxLlpf1yWpsP.U2QjW90lKtcJndsjH3tgBXrLFDrqmAC", 0 },
                    { 2, "jane.smith@example.com", "Jane", "Smith", "$2a$11$Z6qAsCFBaQE9q9klH0ltgu.pWHmJdKdr2FC/rAJ6HN6Iu9cqnJMfm", 0 },
                    { 3, "robert.brown@example.com", "Robert", "Brown", "$2a$11$M65LfuqxWHrRQgZcbt2lTeeOMXtm81IjdY.fyFqu7H0okebvQcYWC", 1 },
                    { 4, "emily.clark@example.com", "Emily", "Clark", "$2a$11$/AvJn5uJm8NntaptoeBo0uGB/i3HuTBLp75xfs0ScaVcGEWmYdI4W", 0 },
                    { 5, "netninja@example.com", "Net", "Ninja", "$2a$11$PSder3iXoPUKb9N5PoDSFOOd9r5oX2LAFe99EWIboH2PVNwOEVAii", 1 }
                });

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "id", "author_id", "description", "image_sourse", "title", "type" },
                values: new object[] { 1, 5, "A comprehensive course on React, covering all the essential topics for web development.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQcR5U16C8yXgBpl7-Bc7Itjx3_LRl425zINA&s", "Full React Course", "Web Development" });

            migrationBuilder.InsertData(
                table: "course_sections",
                columns: new[] { "id", "course_id", "text_source", "title", "video_source" },
                values: new object[,]
                {
                    { 1, 1, "Configurare initiala pentru dezvoltarea aplicatiilor React.", "Setting Up", "https://www.youtube.com/watch?v=xehoCy7k0DE" },
                    { 2, 1, "Bazele JSX: Syntactic sugar pentru JavaScript.", "JSX Basics", "https://www.youtube.com/watch?v=QFaFIcGhPoM" },
                    { 3, 1, "Cum sa construim componente si template-uri in React.", "Components & Templates", "https://www.youtube.com/watch?v=XxVg_s8xAms" },
                    { 4, 1, "Valori dinamice in template-uri React.", "Dynamic Values in Templates", "https://www.youtube.com/watch?v=ftI2bcu_bYk" },
                    { 5, 1, "Bazele evenimentelor in React.", "Event Basics", "https://www.youtube.com/watch?v=kVeOpcw4GWY" },
                    { 6, 1, "Utilizarea hook-ului useState pentru state in React.", "React State (useState hook)", "https://www.youtube.com/watch?v=O6P86uwfdR0" },
                    { 7, 1, "Introducere in formulare cu React.", "Intro to Forms", "https://www.youtube.com/watch?v=owld54-sf6k" },
                    { 8, 1, "Gestionarea formularelor controlate in React.", "Controlled Inputs (forms)", "https://www.youtube.com/watch?v=Pke1S-B0mHM" },
                    { 9, 1, "Bazele hook-ului useEffect in React.", "useEffect Hook (the basics)", "https://www.youtube.com/watch?v=ML7FLpaJszk" },
                    { 10, 1, "Gestionarea dependentelor in useEffect.", "useEffect Dependencies", "https://www.youtube.com/watch?v=dGcsHMXbSOA" },
                    { 11, 1, "Curatarea resurselor cu useEffect.", "Cleaning up with useEffect", "https://www.youtube.com/watch?v=TLr8hdDy7PY" },
                    { 12, 1, "Obtinerea datelor cu hook-ul useEffect.", "Fetching Data with useEffect", "https://www.youtube.com/watch?v=jQc_bTFZ5_I" },
                    { 13, 1, "Afisarea mesajelor de incarcare conditionate.", "Conditional Loading Message", "https://www.youtube.com/watch?v=CVpUuw9XSjY" },
                    { 14, 1, "Gestionarea erorilor la obtinerea datelor.", "Handling Fetch Errors", "https://www.youtube.com/watch?v=Jm6r3nr4ujA" },
                    { 15, 1, "Crearea unui hook personalizat in React.", "Making a Custom Hook", "https://www.youtube.com/watch?v=mDYOg_yerxk" },
                    { 16, 1, "Reutilizarea hook-urilor personalizate.", "Reusing the Custom Hook", "https://www.youtube.com/watch?v=Rpm3aTpEKIY" },
                    { 17, 1, "Utilizarea React Router pentru navigare.", "The React Router", "https://www.youtube.com/watch?v=Law7wfdg_ls" },
                    { 18, 1, "Parametrii rutelor in React Router.", "Route Parameters", "https://www.youtube.com/watch?v=7q0SCZPtKmE" },
                    { 19, 1, "Utilizarea componentei Link din React Router.", "Link Component", "https://www.youtube.com/watch?v=gg6kYL5KBX4" },
                    { 20, 1, "Utilizarea hook-ului useHistory pentru navigare programatica.", "useHistory Hook", "https://www.youtube.com/watch?v=Jh1y-YiXMeE" },
                    { 21, 1, "Redirectionari programatice in React.", "Programmatic Redirects", "https://www.youtube.com/watch?v=IxrDUWDnQaA" },
                    { 22, 1, "Crearea paginilor 404 si pasii urmatori.", "404 Pages & Next Steps", "https://www.youtube.com/watch?v=nL6XMMBsp2E" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_course_sections_course_id",
                table: "course_sections",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_courses_author_id",
                table: "courses",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_courses_users_course_id",
                table: "courses_users",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_questions_section_course_id",
                table: "questions",
                column: "section_course_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_answers_question_id",
                table: "user_answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_answers_user_id",
                table: "user_answers",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses_users");

            migrationBuilder.DropTable(
                name: "user_answers");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "course_sections");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
