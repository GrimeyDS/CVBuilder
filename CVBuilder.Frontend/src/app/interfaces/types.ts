import { HttpContext, HttpHeaders, HttpParams } from "@angular/common/http";

export interface Options {
  headers?:
  | HttpHeaders
  | {
    [header: string]: string | string[];
  };
  observe?: 'body';
  context?: HttpContext;
  params?:
  | HttpParams
  | {
    [param: string]:
    | string
    | number
    | boolean
    | ReadonlyArray<string | number | boolean>;
  };
  reportProgress?: boolean;
  responseType?: 'json';
  withCredentials?: boolean;
  transferCache?:
  | {
    includeHeaders?: string[];
  }
  | boolean;
}

export interface Profiles {
  profiles: Profile[];
}

export interface Profile {
  id: number;
  userId: number;
  firstName: string;
  lastName: string;
  birthDate: Date;
  description: string;
  pictureUrl: string;
  pictureName: string;
  yearsOfExperience: number;
  tags: Tag[];
  experiences: Experience[];
  projects: Project[];
  education: Experience[];
  employment: Experience[];
  currentRole: string;
}

export interface Tags {
  tags: Tag[];
}

export interface Tag {
  id: number;
  title: string;
}

export interface Experiences {
  experiences: Experience[];
}

export interface Experience {
  id: number;
  profileId: number;
  title: string;
  description: string;
  type: string;
  startDate?: Date;
  endDate?: Date;
}

export interface Projects {
  projects: Project[];
}

export interface Project {
  id: number;
  profileId: number;
  title: string;
  description: string;
  customer: string;
  pictureUrl: string;
  pictureName: string;
  startDate?: Date;
  endDate?: Date;
  tags: Tag[];
}

export interface Templates {
  templates: Template[];
}

export interface Template {
  id: number;
  name: string;
}
