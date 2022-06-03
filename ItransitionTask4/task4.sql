--
-- PostgreSQL database dump
--

-- Dumped from database version 14.2
-- Dumped by pg_dump version 14.3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "Id" integer NOT NULL,
    "Email" character varying(60) NOT NULL,
    "Password" character varying(30) NOT NULL,
    "Name" character varying(30) NOT NULL,
    "LastLoginDateTime" character varying(30),
    "RegistrationDateTime" character varying(30),
    "State" integer DEFAULT 0,
    "LoginKey" character varying(64)
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- Name: Users_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Users_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Users_Id_seq" OWNER TO postgres;

--
-- Name: Users_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Users_Id_seq" OWNED BY public."Users"."Id";


--
-- Name: Users Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users" ALTER COLUMN "Id" SET DEFAULT nextval('public."Users_Id_seq"'::regclass);


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Users" ("Id", "Email", "Password", "Name", "LastLoginDateTime", "RegistrationDateTime", "State", "LoginKey") FROM stdin;
16	1	1	1	06/02/2022 21:16:21	06/02/2022 21:16:21	1	69b0dfa59f6cbd6b78eef131f94b99be375b4c12409e4a9ac382defc7b885990
9	mail@mail.ru	1	name	06/02/2022 20:46:39	06/02/2022 20:46:39	1	63685b7a415cb471e963c690575a89473cb6b1927cbe836bb1bf62389d928be1
10	mail222@mail.ru	1	name	06/02/2022 20:49:22	06/02/2022 20:49:22	1	9a23c94d22f674297ef8b0431406504576e4f8d3716eb0bff7da1bf9b14c836b
11	ma@ma.ru	1	name2	06/02/2022 20:56:32	06/02/2022 20:56:32	1	076aa6f9d2c51f70fc6e202a6e003c9c3cd6c2dfc4f58b8efd47426597cb0b38
12	mail1@mail.ru	1	name1	06/02/2022 21:00:55	06/02/2022 21:00:55	1	27f78e53b77c8444a83e14e0fbda46867fe289747d7bad9dd3000bcfd4f12725
15	mail111@m.ru	1	mail111@m.ru	06/02/2022 21:13:19	06/02/2022 21:13:19	1	634dc5766f342ae2c8f705d77b2e3ce013d88911422c25e362f2924452705bda
\.


--
-- Name: Users_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Users_Id_seq"', 17, true);


--
-- Name: Users users_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT users_pk PRIMARY KEY ("Id");


--
-- Name: users_email_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX users_email_uindex ON public."Users" USING btree ("Email");


--
-- Name: users_id_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX users_id_uindex ON public."Users" USING btree ("Id");


--
-- PostgreSQL database dump complete
--

