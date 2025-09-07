export class ResponseIdentiyApiDto {
  succeeded: boolean = false;
  erros: ErrosDto[] = [];
}

export class ErrosDto {
  code?: boolean | null;
  description?: ''
}
